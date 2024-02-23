using NationalInstruments.DAQmx;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OSCiLLOSCOPE
{
    public partial class Form1 : Form
    {

        // DEFINING STORED CHANNELS
        public int[] channels = new int[2];

        //FORM INSTANCE
        public Form1()
        {
            InitializeComponent();
            cbxDevice.Items.AddRange(DaqSystem.Local.Devices);
            if (cbxDevice.Items.Count > 0)
            {
                cbxDevice.SelectedIndex = 0;
            }
            cbxTerminalConfig.Items.AddRange(Enum.GetValues(typeof(AITerminalConfiguration)).Cast<object>().ToArray());
            if (cbxTerminalConfig.Items.Count > 0)
            {
                cbxTerminalConfig.SelectedIndex = 0;
            }
            string[] voltages = { "10", "5", "1", "0.2" };

            cbxVoltageRange.Items.AddRange(voltages);
            if (cbxVoltageRange.Items.Count > 0)
            {
                cbxVoltageRange.SelectedIndex = 0;
            }
            gphData.Titles.Add("Voltage Vs. Time");
            //gphData.ChartAreas[0].AxisX.CustomLabels.Add(200,200,"YOOOO");
            updateNum();

        }

        //WHAT HAPPENS WHEN THE START BUTTON IS CLICKED
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            nudHighChan.Enabled = false;
            nudLowChan.Enabled = false;
            nudSampleChannel.Enabled = false;
            nudSampleRate.Enabled = false;
            btnClearGraph.Enabled = false;

            //Establishing Variables
            int numChan = Convert.ToInt16(nudHighChan.Value) - Convert.ToInt16(nudLowChan.Value) + 1;
            int numSamp = Convert.ToInt32(nudSampleChannel.Value);
            int sampleRate = Convert.ToInt32(nudSampleRate.Value);
            double[,] data = new double[numChan, numSamp];


            try
            {
                //Clearing the graph
                while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }
                // Create a new task and reader
                NationalInstruments.DAQmx.Task analogReadTask = new NationalInstruments.DAQmx.Task();
                AnalogMultiChannelReader reader;
                reader = new AnalogMultiChannelReader(analogReadTask.Stream);
                reader.SynchronizeCallbacks = false;
                //Setting up channels for graph and board
                for (int i = Convert.ToInt16(nudLowChan.Value); i < Convert.ToInt16(nudHighChan.Value + 1); ++i)
                {
                    try
                    {
                        analogReadTask.AIChannels.CreateVoltageChannel(
                            cbxDevice.Text + "/ai" + i.ToString(),
                            "aiChannel" + i.ToString(),
                            (AITerminalConfiguration)(cbxTerminalConfig.SelectedItem),
                            -1 * Convert.ToDouble(cbxVoltageRange.Text),
                            Convert.ToDouble(cbxVoltageRange.Text),
                            AIVoltageUnits.Volts);
                        analogReadTask.Timing.SampleClockRate = sampleRate;
                        analogReadTask.Timing.SamplesPerChannel = numSamp;
                        analogReadTask.Timing.SampleTimingType = SampleTimingType.SampleClock;


                        gphData.Series.Add("Channel" + i.ToString());
                        gphData.Series["Channel" + i.ToString()].ChartType = SeriesChartType.Line;

                        gphData.ChartAreas[0].AxisY.Title = "VOLTAGE (V)";
                        gphData.ChartAreas[0].AxisX.Title = "TIME (T)";
                        gphData.ChartAreas[0].AxisX.MajorTickMark.Interval = 0;
                        gphData.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(cbxVoltageRange.Text) * 1.1;
                        gphData.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(cbxVoltageRange.Text) * -1.1;

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                try
                {
                    IAsyncResult call = reader.BeginReadMultiSample(numSamp, new AsyncCallback(callback), null);
                    data = reader.EndReadMultiSample(call);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                //Establishing a x Time For how Long Recording
                double[] time = new double[numSamp];
                for (int i = 0; i < numSamp; i++)
                {
                    time[i] = Convert.ToDouble(i + 1 / nudSampleRate.Value);
                }

                //Entering Data into the array
                for (int i = 0; i < numChan; i++)
                {
                    for (int j = 0; j < numSamp; j++)
                    {
                        gphData.Series["Channel" + (i + Convert.ToInt16(nudLowChan.Value)).ToString()].Points.AddXY(time[j], data[i, j]);
                    }
                }


                analogReadTask.Dispose();
                btnStart.Enabled = true;
                nudHighChan.Enabled = true;
                nudLowChan.Enabled = true;
                nudSampleChannel.Enabled = true;
                nudSampleRate.Enabled = true;
                btnClearGraph.Enabled = true;
            }

            catch (DaqException exception)
            {
                MessageBox.Show(exception.Message);
            }

            void callback(IAsyncResult ar)
            { }
        }

        private void btnClearGraph_Click(object sender, EventArgs e)
        {
            while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }
        }

        private void updateNum()
        {
            if ((nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1)) > 250000)
            {
                nudSampleRate.Value = 250000 / (nudHighChan.Value - nudLowChan.Value + 1);
            }
            if (nudSampleChannel.Value / (nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1)) > 9)

            {
                nudSampleChannel.Value = 9 * (nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1));
            }
            lblAquisitionTime.Text = Decimal.Round(nudSampleChannel.Value / nudSampleRate.Value, 6).ToString();
            lblADRate.Text = (nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1)).ToString();

        }

        //LOGIC FOR CHANING CHANNELS
        private void ChanChange(object sender, EventArgs e)
        {
            if ((nudHighChan.Value < nudLowChan.Value) & (channels[1] == channels[0])) { }
            else if (nudHighChan.Value >= 15)
            {
                channels[1] = 15;
                channels[0] = Convert.ToInt32(nudLowChan.Value);
            }
            else if (nudLowChan.Value <= 0)
            {
                channels[0] = 0;
                channels[1] = Convert.ToInt32(nudHighChan.Value);
            }
            else
            {
                channels[0] = Convert.ToInt32(nudLowChan.Value);
                channels[1] = Convert.ToInt32(nudHighChan.Value);
            }
            nudHighChan.Value = channels[1];
            nudLowChan.Value = channels[0];
            updateNum();
        }

        //CHANGING SAMPLE RATE OR NUMBER OF SAMPLES
        private void SampleChange(object sender, EventArgs e)
        {
            updateNum();
        }
    }
}
