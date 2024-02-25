using NationalInstruments.DAQmx;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using NationalInstruments.Restricted;

namespace OSCiLLOSCOPE
{
    public partial class Form1 : Form
    {

        // DEFINING STORED CHANNELS
        public int[] channels = new int[2];
        public int numChan;
        public int numSamp;
        public int sampleRate;
        public double[] time;

        public AnalogMultiChannelReader reader;
        public NationalInstruments.DAQmx.Task analogReadTask;
        public IAsyncResult ar;

        //FORM INSTANCE
        public Form1()
        {
            InitializeComponent();
            //creating the Device combo box
            cbxDevice.Items.AddRange(DaqSystem.Local.Devices);
            if (cbxDevice.Items.Count > 0)
            {
                cbxDevice.SelectedIndex = 0;
            }

            //creating the Device terminal config
            cbxTerminalConfig.Items.AddRange(Enum.GetValues(typeof(AITerminalConfiguration)).Cast<object>().ToArray());
            if (cbxTerminalConfig.Items.Count > 0)
            {
                cbxTerminalConfig.SelectedIndex = 0;
            }

            //creating the voltage ranges for the combo boxes.
            string[] voltages = { "10", "5", "1", "0.2" };
            cbxVoltageRange.Items.AddRange(voltages);
            if (cbxVoltageRange.Items.Count > 0)
            {
                cbxVoltageRange.SelectedIndex = 0;
            }
            
            //Adding elements to the chart
            gphData.Titles.Add("Voltage Vs. Time");
            gphData.ChartAreas[0].AxisY.Title = "VOLTAGE (V)";
            gphData.ChartAreas[0].AxisX.Title = "TIME (T)";
            gphData.ChartAreas[0].AxisX.MajorTickMark.Interval = 0;

            //Initializing front pannel components.
            updateNum();
            nudSampleRate.Value = 1000;
            nudSampleChannel.Value = 1000;
        }

        //WHAT HAPPENS WHEN THE START BUTTON IS CLICKED
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Update the buttons.
            btnUpdate(false);

            //Establishing Variables
            int numChan = Convert.ToInt16(nudHighChan.Value) - Convert.ToInt16(nudLowChan.Value) + 1;
            int numSamp = Convert.ToInt32(nudSampleChannel.Value);
            int sampleRate = Convert.ToInt32(nudSampleRate.Value);
            double[,] data = new double[numChan, numSamp];


            try
            {
                //Clearing the graph
                while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }

                // Create a new task
                NationalInstruments.DAQmx.Task analogReadTask = new NationalInstruments.DAQmx.Task();     
                
                //creating channels and chart areas
                for (int i = Convert.ToInt16(nudLowChan.Value); i < Convert.ToInt16(nudHighChan.Value + 1); ++i)
                {
                    //creating channels
                    analogReadTask.AIChannels.CreateVoltageChannel(
                        cbxDevice.Text + "/ai" + i.ToString(),
                       "aiChannel" + i.ToString(),
                        (AITerminalConfiguration)(cbxTerminalConfig.SelectedItem),
                        -1 * Convert.ToDouble(cbxVoltageRange.Text),
                        Convert.ToDouble(cbxVoltageRange.Text),
                        AIVoltageUnits.Volts);

                    // Adding series to chart
                    gphData.Series.Add("Channel" + i.ToString());
                    gphData.Series["Channel" + i.ToString()].ChartType = SeriesChartType.Line;
                    gphData.ChartAreas[0].AxisY.Title = "VOLTAGE (V)";
                    gphData.ChartAreas[0].AxisX.Title = "TIME (T)";
                    gphData.ChartAreas[0].AxisX.MajorTickMark.Interval = 0;
                    gphData.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(cbxVoltageRange.Text) * 1.1;
                    gphData.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(cbxVoltageRange.Text) * -1.1;
                }
                //Setting the timing
                analogReadTask.Timing.SampleClockRate = sampleRate;
                analogReadTask.Timing.SamplesPerChannel = numSamp;

                //creating the reader
                reader = new AnalogMultiChannelReader(analogReadTask.Stream);
                reader.BeginReadMultiSample(numSamp, new AsyncCallback(callback), null);

                //Update the buttons
                btnUpdate(true);
            }
            catch (DaqException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //Asynchronous Callback function
        private void callback(IAsyncResult ar)
        {
            try
            {
                //Establish the variables
                int numChan = Convert.ToInt16(nudHighChan.Value) - Convert.ToInt16(nudLowChan.Value) + 1;
                int numSamp = Convert.ToInt32(nudSampleChannel.Value);
                int sampleRate = Convert.ToInt32(nudSampleRate.Value);

                // Data Collection
                double[,] data = reader.EndReadMultiSample(ar);

                // creating time array
                double[] time = new double[numSamp];
                for (int i = 0; i < numSamp; i++)
                {
                    time[i] = Convert.ToDouble(i + 1 / nudSampleRate.Value) / 1000;
                }

                // Adding data to graph
                for (int i = 0; i < numChan; i++)
                {
                    for (int j = 0; j < numSamp; j++)
                    {
                        gphData.Series["Channel" + (i + Convert.ToInt16(nudLowChan.Value)).ToString()].Points.AddXY(time[j], data[i, j]);
                    }
                }
            }
            catch (DaqException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (analogReadTask != null)
                {
                    analogReadTask.Dispose();
                }
            }
        }

        //Update Button Function
        private void btnUpdate(bool a)
        {
            btnStart.Enabled = a;
            nudHighChan.Enabled = a;
            nudLowChan.Enabled = a;
            nudSampleChannel.Enabled = a;
            nudSampleRate.Enabled = a;
            btnClearGraph.Enabled = a;
        }

        //Clearing the Graph
        private void btnClearGraph_Click(object sender, EventArgs e)
        {
            while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }
        }

        //Updateing the front pannel numbers.
        private void updateNum()
        {
            if ((nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1)) > 250000)
            {
                nudSampleRate.Value = 250000 / (nudHighChan.Value - nudLowChan.Value + 1);
            }
            if (nudSampleChannel.Value / (nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1)) > 9)

            {
                nudSampleChannel.Value = 9 * (nudSampleRate.Value);
            }
            lblAquisitionTime.Text = Decimal.Round(nudSampleChannel.Value / nudSampleRate.Value, 6).ToString();
            lblADRate.Text = Decimal.Round(nudSampleRate.Value * (nudHighChan.Value - nudLowChan.Value + 1), 0).ToString();

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
