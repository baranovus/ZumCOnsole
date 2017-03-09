using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using ZumConsole.Properties;
using System.Windows.Forms;

namespace ZumConsole
{
    public partial class Form3 : Form
    {
        enum rfchannels
        {
            ALL = 0, CH11 = 11, CH12 = 12, CH13 = 13, CH14 = 14, CH15 = 15, CH16 = 16,
            CH17 = 17, CH18 = 18, CH19 = 19, CH20 = 20, CH21 = 21, CH22 = 22,
            CH23 = 23, CH24 = 24, CH25 = 25, CH26 = 26
        };
        uint CurrentRFchannel = (uint)rfchannels.ALL;
        List<RFData> rfdata = new List<RFData>();
        TextAnnotation ann = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation();
        String Hostname = String.Empty;
        Int32 PortNumber = 41795;
        NetworkStream tcp_stream;
        NetworkConn net_conn = new NetworkConn();
        delegate void SetTextCallback(string text);
        bool tcp_connected = false;
        public Form3()
        {
            InitializeComponent();
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
            backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker2_ProgressChanged);
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void ChartInitialize()
        {
            chart1.Series["Average"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            chart1.Series["Peak"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            chart1.ChartAreas[0].AxisX.Minimum = 10;
            chart1.ChartAreas[0].AxisX.Maximum = 27;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 90;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisX.Title = "Channel";
            chart1.ChartAreas[0].AxisY.Title = "dBm";
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.ChartAreas[0].BorderColor = Color.Black;
            chart1.Legends[0].TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Wide;
            chart1.Legends[0].LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.ReversedSeriesOrder;


            StripLine sline = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            sline.Interval = 80;
            sline.Text = "Danger Zone";
            sline.TextAlignment = StringAlignment.Center;
            sline.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            sline.BorderColor = Color.Red;
            sline.ForeColor = Color.Red;
            sline.ToolTip = "TBD Help";
            sline.BorderWidth = 2;
            chart1.ChartAreas[0].AxisY.StripLines.Add(sline);

            ann.Text = "Scanning...";
            ann.Font = new Font(ann.Font.FontFamily.Name, 24);
            ann.ForeColor = Color.DarkRed;
            ann.X = 40;
            ann.Y = 22;
            ann.Visible = false;
            chart1.Annotations.Add(ann);

        }
        private void DataInitialize()
        {
            rfdata.Clear();
            for (int i = 0; i < 16; i++)
            {
                rfdata.Add(new RFData(i + (int)(rfchannels.CH11), -89, -89));
            }
            ChartUpdate();
        }
        private void ChartUpdate()
        {
             chart1.Series["Average"].Points.Clear();
            chart1.Series["Peak"].Points.Clear();
            foreach (RFData p in rfdata)
            {
                chart1.Series["Peak"].Points.AddXY(p.GetChannel(), p.GetPeak() - p.GetAverage());
                chart1.Series["Peak"].Points.Last().ToolTip = p.GetPeak() + "dBm";
                chart1.Series["Average"].Points.AddXY(p.GetChannel(), 90 + p.GetAverage());
                chart1.Series["Average"].Points.Last().ToolTip = p.GetAverage() + "dBm";
            }

        }
        private void ChartCustomize()
        {
            for (int i = -5, j = -90; i < 90; i += 10, j += 10)
            {
                chart1.ChartAreas[0].AxisY.CustomLabels.Add(i, i + 10, "" + j);
            }

        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ann.Visible = false;
            ScanButton.Enabled = true;
 
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //            this.resultLabel.Text = e.ProgressPercentage.ToString();
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;

            }
            else
            {
                e.Result = 0;// SendScanCommandAndGetResponse(worker, e);
            }
        }
        private void SetHostNameText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true.
            if (this.HostNameLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetHostNameText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.HostNameLabel.Text = text;
            }
        }
        private void ConnSettings3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            // Add an event handler to update this form
            // when the ID form is updated (when IdentityUpdated fires).
            form2.NetParametersUpdated += new Form2.NetUpdateHandler(Net_Settings_ButtonClicked);
            form2.Show();
        }
        private void Net_Settings_ButtonClicked(object sender, NetUpdateEventArgs e)
        {
            // update the forms values from the event args
            Hostname = e.HostName;
            String port_str = e.PortName;
            PortNumber = Convert.ToInt32(port_str, 10);
            tcp_stream = net_conn.MakeConnection(Hostname, (int)PortNumber);
            tcp_connected = net_conn.GetTcpConnected();
            if (tcp_connected)
            {
                SetHostNameText(Hostname + ":" + PortNumber);
            }
            else
            {
                SetHostNameText("Failed to connect to TCP");
            }

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            net_conn.CloseConnection();
        }
  
    }
}
