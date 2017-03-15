namespace ZumConsole
{
    partial class Form3
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.RFChannelBox = new System.Windows.Forms.ComboBox();
            this.RF_channel_label = new System.Windows.Forms.Label();
            this.ScanButton = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.ConnSettings3 = new System.Windows.Forms.Button();
            this.HostNameLabel = new System.Windows.Forms.Label();
            this.DiagLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(197, -6);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Average";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Peak";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(886, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // RFChannelBox
            // 
            this.RFChannelBox.FormattingEnabled = true;
            this.RFChannelBox.Items.AddRange(new object[] {
            "All",
            "Channel 11",
            "Channel 12",
            "Channel 13",
            "Channel 14",
            "Channel 15",
            "Channel 16",
            "Channel 17",
            "Channel 18",
            "Channel 19",
            "Channel 20",
            "Channel 21",
            "Channel 22",
            "Channel 23",
            "Channel 24",
            "Channel 25",
            "Channel 26"});
            this.RFChannelBox.Location = new System.Drawing.Point(20, 151);
            this.RFChannelBox.Name = "RFChannelBox";
            this.RFChannelBox.Size = new System.Drawing.Size(135, 21);
            this.RFChannelBox.TabIndex = 0;
            this.RFChannelBox.SelectedIndexChanged += new System.EventHandler(this.RFChannelBox_SelectedIndexChanged);
            // 
            // RF_channel_label
            // 
            this.RF_channel_label.AutoSize = true;
            this.RF_channel_label.Location = new System.Drawing.Point(24, 125);
            this.RF_channel_label.Name = "RF_channel_label";
            this.RF_channel_label.Size = new System.Drawing.Size(62, 13);
            this.RF_channel_label.TabIndex = 2;
            this.RF_channel_label.Text = "RF channel";
            // 
            // ScanButton
            // 
            this.ScanButton.Location = new System.Drawing.Point(32, 202);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(123, 25);
            this.ScanButton.TabIndex = 3;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // ConnSettings3
            // 
            this.ConnSettings3.Location = new System.Drawing.Point(27, 19);
            this.ConnSettings3.Name = "ConnSettings3";
            this.ConnSettings3.Size = new System.Drawing.Size(127, 25);
            this.ConnSettings3.TabIndex = 4;
            this.ConnSettings3.Text = "Comm Settings";
            this.ConnSettings3.UseVisualStyleBackColor = true;
            this.ConnSettings3.Click += new System.EventHandler(this.ConnSettings3_Click);
            // 
            // HostNameLabel
            // 
            this.HostNameLabel.AutoSize = true;
            this.HostNameLabel.Location = new System.Drawing.Point(28, 65);
            this.HostNameLabel.Name = "HostNameLabel";
            this.HostNameLabel.Size = new System.Drawing.Size(0, 13);
            this.HostNameLabel.TabIndex = 5;
            // 
            // DiagLabel
            // 
            this.DiagLabel.AutoSize = true;
            this.DiagLabel.Location = new System.Drawing.Point(27, 320);
            this.DiagLabel.Name = "DiagLabel";
            this.DiagLabel.Size = new System.Drawing.Size(0, 13);
            this.DiagLabel.TabIndex = 6;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 355);
            this.Controls.Add(this.DiagLabel);
            this.Controls.Add(this.HostNameLabel);
            this.Controls.Add(this.ConnSettings3);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.RF_channel_label);
            this.Controls.Add(this.RFChannelBox);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "Energy Scan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox RFChannelBox;
        private System.Windows.Forms.Label RF_channel_label;
        private System.Windows.Forms.Button ScanButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button ConnSettings3;
        private System.Windows.Forms.Label HostNameLabel;
        private System.Windows.Forms.Label DiagLabel;
    }
}