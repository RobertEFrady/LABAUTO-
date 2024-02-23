namespace OSCiLLOSCOPE
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTerminalConfig = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearGraph = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbxVoltageRange = new System.Windows.Forms.ComboBox();
            this.lblADRate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAquisitionTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudSampleRate = new System.Windows.Forms.NumericUpDown();
            this.nudSampleChannel = new System.Windows.Forms.NumericUpDown();
            this.nudLowChan = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudHighChan = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gphData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSampleChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLowChan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHighChan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gphData)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxDevice
            // 
            this.cbxDevice.FormattingEnabled = true;
            this.cbxDevice.Location = new System.Drawing.Point(12, 62);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(206, 33);
            this.cbxDevice.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = " Device :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(8, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Terminal Configuration :";
            // 
            // cbxTerminalConfig
            // 
            this.cbxTerminalConfig.FormattingEnabled = true;
            this.cbxTerminalConfig.Location = new System.Drawing.Point(13, 136);
            this.cbxTerminalConfig.Name = "cbxTerminalConfig";
            this.cbxTerminalConfig.Size = new System.Drawing.Size(205, 33);
            this.cbxTerminalConfig.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(15, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "CHANNEL RANGE";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Controls.Add(this.btnClearGraph);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.cbxVoltageRange);
            this.groupBox1.Controls.Add(this.lblADRate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblAquisitionTime);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbxDevice);
            this.groupBox1.Controls.Add(this.nudSampleRate);
            this.groupBox1.Controls.Add(this.nudSampleChannel);
            this.groupBox1.Controls.Add(this.nudLowChan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nudHighChan);
            this.groupBox1.Controls.Add(this.cbxTerminalConfig);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 498);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DAQ CONFIG";
            // 
            // btnClearGraph
            // 
            this.btnClearGraph.Location = new System.Drawing.Point(288, 409);
            this.btnClearGraph.Name = "btnClearGraph";
            this.btnClearGraph.Size = new System.Drawing.Size(151, 71);
            this.btnClearGraph.TabIndex = 21;
            this.btnClearGraph.Text = "Clear Graph";
            this.btnClearGraph.UseVisualStyleBackColor = true;
            this.btnClearGraph.Click += new System.EventHandler(this.btnClearGraph_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(33, 409);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(151, 71);
            this.btnStart.TabIndex = 20;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbxVoltageRange
            // 
            this.cbxVoltageRange.FormattingEnabled = true;
            this.cbxVoltageRange.Location = new System.Drawing.Point(260, 62);
            this.cbxVoltageRange.Name = "cbxVoltageRange";
            this.cbxVoltageRange.Size = new System.Drawing.Size(206, 33);
            this.cbxVoltageRange.TabIndex = 19;
            // 
            // lblADRate
            // 
            this.lblADRate.AutoSize = true;
            this.lblADRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblADRate.Location = new System.Drawing.Point(296, 372);
            this.lblADRate.Name = "lblADRate";
            this.lblADRate.Size = new System.Drawing.Size(0, 20);
            this.lblADRate.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(255, 346);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 26);
            this.label11.TabIndex = 17;
            this.label11.Text = "A/D Rate (S/s)";
            // 
            // lblAquisitionTime
            // 
            this.lblAquisitionTime.AutoSize = true;
            this.lblAquisitionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblAquisitionTime.Location = new System.Drawing.Point(296, 297);
            this.lblAquisitionTime.Name = "lblAquisitionTime";
            this.lblAquisitionTime.Size = new System.Drawing.Size(0, 20);
            this.lblAquisitionTime.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(255, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(226, 26);
            this.label9.TabIndex = 15;
            this.label9.Text = "AQUISITION TIME (s)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(256, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(202, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "SAMPLES PER CHANNEL";
            // 
            // nudSampleRate
            // 
            this.nudSampleRate.Location = new System.Drawing.Point(258, 136);
            this.nudSampleRate.Maximum = new decimal(new int[] {
            250000,
            0,
            0,
            0});
            this.nudSampleRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSampleRate.Name = "nudSampleRate";
            this.nudSampleRate.Size = new System.Drawing.Size(208, 32);
            this.nudSampleRate.TabIndex = 6;
            this.nudSampleRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSampleRate.ValueChanged += new System.EventHandler(this.SampleChange);
            // 
            // nudSampleChannel
            // 
            this.nudSampleChannel.Location = new System.Drawing.Point(258, 206);
            this.nudSampleChannel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSampleChannel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSampleChannel.Name = "nudSampleChannel";
            this.nudSampleChannel.Size = new System.Drawing.Size(208, 32);
            this.nudSampleChannel.TabIndex = 13;
            this.nudSampleChannel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSampleChannel.ValueChanged += new System.EventHandler(this.SampleChange);
            // 
            // nudLowChan
            // 
            this.nudLowChan.Location = new System.Drawing.Point(17, 271);
            this.nudLowChan.Name = "nudLowChan";
            this.nudLowChan.Size = new System.Drawing.Size(201, 32);
            this.nudLowChan.TabIndex = 7;
            this.nudLowChan.ValueChanged += new System.EventHandler(this.ChanChange);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.Location = new System.Drawing.Point(19, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "HIGH CHANNEL";
            // 
            // nudHighChan
            // 
            this.nudHighChan.Location = new System.Drawing.Point(17, 346);
            this.nudHighChan.Name = "nudHighChan";
            this.nudHighChan.Size = new System.Drawing.Size(201, 32);
            this.nudHighChan.TabIndex = 8;
            this.nudHighChan.ValueChanged += new System.EventHandler(this.ChanChange);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(16, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "LOW CHANNEL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(253, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "VOLTAGE RANGE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(254, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "CHANNEL SAMPLE RATE";
            // 
            // gphData
            // 
            chartArea1.Name = "ChartArea1";
            this.gphData.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.gphData.Legends.Add(legend1);
            this.gphData.Location = new System.Drawing.Point(561, 33);
            this.gphData.Name = "gphData";
            this.gphData.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            this.gphData.Size = new System.Drawing.Size(537, 427);
            this.gphData.TabIndex = 7;
            this.gphData.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 492);
            this.Controls.Add(this.gphData);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSampleChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLowChan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHighChan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gphData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxTerminalConfig;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudSampleRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudHighChan;
        private System.Windows.Forms.NumericUpDown nudLowChan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudSampleChannel;
        private System.Windows.Forms.Label lblADRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAquisitionTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataVisualization.Charting.Chart gphData;
        private System.Windows.Forms.ComboBox cbxVoltageRange;
        private System.Windows.Forms.Button btnClearGraph;
        private System.Windows.Forms.Button btnStart;
    }
}

