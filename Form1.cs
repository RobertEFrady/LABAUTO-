using NationalInstruments.DAQmx;
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using NationalInstruments.Restricted;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OSCiLLOSCOPE
{

    public partial class Form1 : Form
    {

        // DEFINING STORED CHANNELS
        public int[] channels = new int[2];
        public int numChan;
        public int numSamp;
        public int sampleRate;
        NationalInstruments.DAQmx.Task dTask;
        public IAsyncResult ar;
        public DataCollecter dataCollecter = new DataCollecter();
        public SaveFileDialog saveFileDialog;
        public OpenFileDialog openFileDialog;

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

            Globals.form = this;
        }

        //WHAT HAPPENS WHEN THE START BUTTON IS CLICKED
        private void btnStart_Click(object sender, EventArgs e)
        {
            int numChan = Convert.ToInt32(nudHighChan.Value) - Convert.ToInt32(nudLowChan.Value) + 1;
            int numSamp = Convert.ToInt32(nudSampleChannel.Value);

            //Update the buttons.
            btnUpdate(false);
            dataCollecter.cbxDevice = cbxDevice.Text;
            dataCollecter.cbxTerminalConfig = (AITerminalConfiguration)cbxTerminalConfig.SelectedItem;
            dataCollecter.cbxVoltageRange = cbxVoltageRange.Text;
            dataCollecter.nudLowChan = (int)nudLowChan.Value;
            dataCollecter.nudHighChan = (int)nudHighChan.Value;
            dataCollecter.nudSampleChannel = (int)nudSampleChannel.Value;
            dataCollecter.nudSampleRate = (int)nudSampleRate.Value;
            dataCollecter.CollectData();

            
        }
        public void chtUpdate()
        {
            int numChan = Convert.ToInt32(nudHighChan.Value) - Convert.ToInt32(nudLowChan.Value) + 1;
            int numSamp = Convert.ToInt32(nudSampleChannel.Value);
            while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }
            for (int i = dataCollecter.nudLowChan; i < (dataCollecter.nudHighChan + 1); ++i)
            {
                gphData.Series.Add("Channel" + i.ToString());
                gphData.Series["Channel" + i.ToString()].ChartType = SeriesChartType.Line;
                gphData.ChartAreas[0].AxisY.Title = "VOLTAGE (V)";
                gphData.ChartAreas[0].AxisX.Title = "TIME (T)";
                gphData.ChartAreas[0].AxisX.MajorTickMark.Interval = 0;
                gphData.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(dataCollecter.cbxVoltageRange) * 1.1;
                gphData.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(dataCollecter.cbxVoltageRange) * -1.1;
            }
            for (int i = 0; i < numSamp; i++)
            {
                for (int j = 0; j < numChan; j++)
                {
                    gphData.Series["Channel" + (j + dataCollecter.nudLowChan).ToString()].Points.AddXY(
                        dataCollecter.dataNow[i].time, dataCollecter.dataNow[i].data[j]);
                }
            }
        }
        //Update Button Function
        public void btnUpdate(bool a)
        {
            btnStart.Enabled = a;
            nudHighChan.Enabled = a;
            nudLowChan.Enabled = a;
            nudSampleChannel.Enabled = a;
            nudSampleRate.Enabled = a;
            btnClearGraph.Enabled = a;
            menuStrip1.Enabled = a;
        }

        //Clearing the Graph
        private void btnClearGraph_Click(object sender, EventArgs e)
        {
            while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }
        }

        //Updateing the front pannel numbers.
        private void updateNum()
        {
            int numChan = Convert.ToInt32(nudHighChan.Value) - Convert.ToInt32(nudLowChan.Value) + 1;
            int numSamp = Convert.ToInt32(nudSampleChannel.Value);
            int sampleRate = Convert.ToInt32(nudSampleRate.Value);
            if ((sampleRate * numChan) > 250000) // AD RATE LIMITER
            {
                nudSampleRate.Value = 250000 / (numChan);
            }
            if (numSamp / sampleRate > 9) // MAX TIME LIMITER

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

        private void clearChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (gphData.Series.Count > 0) { gphData.Series.RemoveAt(0); }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please View Common Queries On The Git Page");
            System.Diagnostics.Process.Start("https://github.com/RobertEFrady/LABAUTO-/tree/Menu-Items");
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataCollecter.SaveDataAs(sender, e);
        }

        private void appendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataCollecter.SaveDataAppend(sender, e);
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataCollecter.OpenDataFile();
        }
    }
    public class DataStorage
    {
        public List<int> chan { get; set; } = new List<int>();
        public List<double> data { get; set; } = new List<double>();
        public double time { get; set; }
    }
    public class DataCollecter
    {
        public bool flag = true;
        public int nudLowChan { get; set; }
        public int nudHighChan { get; set; }
        public int nudSampleChannel { get; set; }
        public double nudSampleRate { get; set; }
        public string cbxDevice { get; set; }
        public int numSamp {  get; set; }
        public int numChan { get; set; }
        public AITerminalConfiguration cbxTerminalConfig { get; set; }
        public string cbxVoltageRange { get; set; }
        public List<DataStorage> dataNow = new List<DataStorage>();
        NationalInstruments.DAQmx.Task analogReadTask;
        NationalInstruments.DAQmx.Task subTask;
        public AnalogMultiChannelReader reader;
        public void CollectData()
        {

            flag = false;
            dataNow.Clear();
            numChan = Convert.ToInt32(nudHighChan) - Convert.ToInt32(nudLowChan) + 1;
            numSamp = Convert.ToInt32(nudSampleChannel);
            int sampleRate = Convert.ToInt32(nudSampleRate);
            double[,] data = new double[numChan, numSamp];
            Console.WriteLine("ALOHA 1");
            NationalInstruments.DAQmx.Task analogReadTask = new NationalInstruments.DAQmx.Task();

            analogReadTask.SynchronizeCallbacks = true;
            //try
            //{
            //creating channels and chart areas
            for (int i = Convert.ToInt16(nudLowChan); i < Convert.ToInt16(nudHighChan + 1); ++i)
            {
                //creating channels
                analogReadTask.AIChannels.CreateVoltageChannel(
                    cbxDevice + "/ai" + i.ToString(),
                   "aiChannel" + i.ToString(),
                    (AITerminalConfiguration)(cbxTerminalConfig),
                    -1 * Convert.ToDouble(cbxVoltageRange),
                    Convert.ToDouble(cbxVoltageRange),
                    AIVoltageUnits.Volts);
                //Setting the timing
                analogReadTask.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples);
                //creating the reader
                reader = new AnalogMultiChannelReader(analogReadTask.Stream);
            }
            subTask = analogReadTask;
            reader.BeginReadMultiSample(numSamp, new AsyncCallback(callback), null);
            Console.WriteLine("ALOHA 2");

            // }
            //catch (Exception e) { MessageBox.Show(e.Message); }
        }
        public void callback(IAsyncResult ar)
        {
            numChan = Convert.ToInt32(nudHighChan) - Convert.ToInt32(nudLowChan) + 1;
            numSamp = Convert.ToInt32(nudSampleChannel);
            Console.WriteLine("ALOHA 3");
            try
            {
                //data collection
                Console.WriteLine("ALOHA 4");
                
                double[,] data = reader.EndReadMultiSample(ar);
                Console.WriteLine("ALOHA 5");
                //Filling list of data objects with data
                for (int j = 0; j < numSamp; j++)
                {
                    DataStorage temp = new DataStorage();
                    temp.time = Convert.ToDouble((j + 1) / nudSampleRate);
                    for (int i = 0; i < numChan; i++)
                    {
                        temp.chan.Add(i+nudLowChan);
                        temp.data.Add(data[i, j]);
                    }
                    dataNow.Add(temp);
                }
            }
            catch (DaqException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                try
                {
                    Console.WriteLine("ALOHA 6");
                    flag = true;
                    subTask.Dispose();
                }                   
                catch (Exception ex) { MessageBox.Show(ex.Message  ); }
            Globals.form.chtUpdate();
            Globals.form.btnUpdate(true);
            }
        }
        static void DisposeTask(NationalInstruments.DAQmx.Task task)
        {
            task?.Stop();
            task?.Dispose();
        }
        
        public void OpenDataFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckFileExists = true;
            dialog.CreatePrompt = false;
            dialog.OverwritePrompt = false;
            dialog.InitialDirectory = "C:\\Users\\rober\\OneDrive\\Desktop\\";
            dialog.Title = "Save csv Files";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            List<DataStorage> dataOpen = new List<DataStorage>();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                List<string> temp =  File.ReadLines(file).ToList();
                temp.RemoveRange(0, 3);
                int lowChan = Convert.ToInt16(temp[0].Split(',')[1].Remove(0,5));
                nudLowChan = lowChan;
                
                temp.RemoveAt(0);
                try
                {
                    
                    foreach (var row in temp)
                    {
                        DataStorage dataTemp = new DataStorage();
                        string[] each = row.Split(',');
                        int indexer = lowChan;
                        dataTemp.time = Convert.ToDouble(each[0]);
                        List<String> strings = new List<String>();
                        strings = each.ToList();
                        strings.RemoveAt(0);
                        nudHighChan = lowChan + strings.Count-1;
                        foreach (string eachValue in strings)
                        {
                            
                            dataTemp.data.Add(Convert.ToDouble(eachValue));
                            dataTemp.chan.Add(indexer++);
                            indexer++;
                        }
                        dataOpen.Add(dataTemp);
                    }
                    dataNow = dataOpen;
                    for (int i = 0; i < 11; i++) 
                    {
                        if (dataNow.Where(o => o.data.Max() >i).Count() ==0)
                        {
                            cbxVoltageRange = i.ToString();
                            break;
                        }
                    }
                    
                    Globals.form.chtUpdate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Parse File");
                    MessageBox.Show(ex.Message);
                }

            }
        }

        public void SaveDataAppend(object sender,EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckFileExists = true;
            dialog.CreatePrompt = false;
            dialog.OverwritePrompt = false;
            dialog.InitialDirectory = "C:\\Users\\rober\\OneDrive\\Desktop\\";
            dialog.Title = "Save csv Files";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;

                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = xlApp.Workbooks.Add(Type.Missing);
                StringBuilder output = new StringBuilder();
                String separator = ",";
                List<string> newLine = new List<string>();
                //Line 1
                output.AppendLine(String.Join(separator + "Date ", System.DateTime.Today));

                //Line 2
                output.AppendLine(String.Join(separator + "Time ", System.DateTime.Now));
                //Line 1
                output.AppendLine(String.Join(separator + "Count ", dataNow.Count));

                //Line 4
                newLine = new List<string>();
                newLine.Add("Total Time " + dataNow.Last().time.ToString());
                foreach (var chan in dataNow[0].chan)
                {
                    newLine.Add("Chan " + chan.ToString());
                }
                output.AppendLine(String.Join(separator, newLine));

                //Line 5 onward
                foreach (var line in dataNow)
                {
                    newLine = new List<string>();
                    newLine.Add(line.time.ToString());
                    foreach (var value in line.data)
                    {
                        newLine.Add(value.ToString());
                    }

                    output.AppendLine(string.Join(separator, newLine));

                }
                try
                {
                    File.AppendAllText(file, output.ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        public void SaveDataAs(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckFileExists = false;
            dialog.CreatePrompt = true;
            dialog.InitialDirectory = "C:\\Users\\rober\\OneDrive\\Desktop\\";
            dialog.Title = "Save csv Files";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;

                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = xlApp.Workbooks.Add(Type.Missing);
                StringBuilder output = new StringBuilder();
                String separator = ",";
                List<string> newLine = new List<string>();
                //Line 1
                output.AppendLine(String.Join(separator+"Date ", System.DateTime.Today));

                //Line 2
                output.AppendLine(String.Join(separator+"Time ", System.DateTime.Now));
                //Line 1
                output.AppendLine(String.Join(separator+"Count ", dataNow.Count));

                //Line 4
                newLine = new List<string>();
                newLine.Add("Total Time "+dataNow.Last().time.ToString());
                foreach (var chan in dataNow[0].chan)
                {
                    newLine.Add("Chan "+ chan.ToString());
                }
                output.AppendLine(String.Join(separator, newLine));

                //Line 5 onward
                foreach (var line in dataNow)
                {
                    newLine = new List<string>();
                    newLine.Add(line.time.ToString());
                    foreach (var value in line.data)
                    {
                        newLine.Add(value.ToString());
                    }

                    output.AppendLine(string.Join(separator, newLine));

                }
                try
                {
                    File.Create(file).Close();
                    File.AppendAllText(file, output.ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
    public class Globals
    {
        public static Form1 form;
    }
}