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
        Int32 Form3NeworkConnectionOwner = 3;
        uint CurrentRFchannel = (uint)rfchannels.ALL;
  
        List<RFData> rfdata = new List<RFData>();
        TextAnnotation ann = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation();
        String Hostname = String.Empty;
        Int32 PortNumber = 41795;
        NetworkStream tcp_stream;

        NetworkConn net_conn = NetworkConn.Instance;    //getting instance of NetworkConn singleton class

        delegate void SetTextCallback(string text);
 /***************** Create event of form closing**********/
        public delegate void ScanFormClosingHandler(object sender, ScanFormCloseEventArgs e);
        public event ScanFormClosingHandler ScanFormClosing;        // add an event of the delegate type on form close
/***********************************************************/
        bool tcp_connected = false;
        private String tcpresponse = String.Empty;
        private byte[] txbuffer = new byte[2000];           //transmit buffer for tcp
        private byte[] rxbuffer = new byte[5000];           //receive buffer for tcp

        public Form3()
        {
            InitializeComponent();
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
            backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker2_ProgressChanged);
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.RFChannelBox.SelectedIndex = 0;
            DataInitialize();
            ChartInitialize();
            ChartCustomize();
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

 /******** Singleton proba
            SingleGlobal singleton = SingleGlobal.Instance;
            string vvv = singleton.GetGlobal();
 /*************************/
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
            ParseRxString();
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
                e.Result =  SendScanCommandAndGetResponse(worker, e);
            }
        }
        private void SetDiagText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true.
            if (this.DiagLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDiagText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.DiagLabel.Text = text;
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
            ConnSettings3.Enabled = false;
            // Add an event handler to update this form
            // when the ID form is updated (when IdentityUpdated fires).
            form2.NetParametersUpdated += new Form2.NetUpdateHandler(Net_Settings_ButtonClicked);
            form2.ConnFormClosing += new Form2.ConnFormClosingHandler(ConnFormClosed);
            form2.Show();
        }

        private void ConnFormClosed(object sender, ConnFormCloseEventArgs e)
        {
            ConnSettings3.Enabled = true;

        }
        
        private void Net_Settings_ButtonClicked(object sender, NetUpdateEventArgs e)
        {
            // update the forms values from the event args
            
            if (!net_conn.GetTcpConnected())
            {
                Hostname = e.HostName;
                String port_str = e.PortName;
                PortNumber = Convert.ToInt32(port_str, 10);
                tcp_stream = net_conn.MakeConnection(Hostname, (int)PortNumber, Form3NeworkConnectionOwner);
                if (net_conn.GetTcpConnected())
                {
                    SetHostNameText(Hostname + ":" + PortNumber);
                }
                else
                {
                    SetHostNameText("Failed to connect to TCP");
                }
            }
            else
            {
                tcp_stream = net_conn.GetStream();
                Hostname = net_conn.GetHostName();
                PortNumber = net_conn.GetPort();
                SetHostNameText(Hostname + ":" + PortNumber);
            }
            tcp_connected = net_conn.GetTcpConnected();
        }

        private int SendScanCommandAndGetResponse(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                String scancmd = MakeScanCommand(CurrentRFchannel);
                SendStringToTCP(tcp_stream, ref scancmd, ref tcpresponse, 14000);
                this.backgroundWorker2.CancelAsync();
            }
            return 0;
        }

        private String MakeScanCommand(uint channel)
        {
            String cmd = "ENERGYSCAN ALL ON " + channel + " 1 8\r\n";
            return cmd;
        }


        private void ParseRxString()
        {
            int channel = 0;
            double average = 0, peak = 0;
            int r1 = 0;
            string sr = null;
            string[] r = new string[20];
            int index = tcpresponse.IndexOf("count", 0);
            if (index < 0) { return; }

            int dbm_index = 0;
            for (uint i = 0; i < r.Length; i++)
            {
                index = tcpresponse.IndexOf(":", index) - 2;
                if (index < 0) break;
                dbm_index = tcpresponse.IndexOf("[dBm]", index);
                if (dbm_index < 0) break;
                r[i] = tcpresponse.Substring(index, dbm_index - index);
                index = dbm_index;
            }
            for (uint i = 0; i < r.Length; i++)
            {
                if (r[i] != null)
                {
                    index = r[i].IndexOf(":", 0);
                    sr = r[i].Substring(0, index);
                    channel = Convert.ToInt16(sr);
                    index = r[i].IndexOf("Avg", 0) + "Avg".Length;
                    r1 = r[i].IndexOf(",", 0);
                    sr = r[i].Substring(index, r1 - index);
                    average = Convert.ToInt32(sr);
                    index = r[i].IndexOf("Peak", 0) + "Peak".Length;
                    sr = r[i].Substring(index);
                    peak = Convert.ToInt32(sr);
                    if (channel >= (int)rfchannels.CH11)
                    {
                        rfdata[channel - (int)rfchannels.CH11].SetChannel(channel);
                        rfdata[channel - (int)rfchannels.CH11].SetAverage(average);
                        rfdata[channel - (int)rfchannels.CH11].SetPeak(peak);
                    }
                }
            }
            ChartUpdate();
        }
        private int WaitForResponseFromTCP(NetworkStream stream)
        {
            int nTry = 1000;
            Int32 res = -1;
            while (!stream.DataAvailable)
            {
                Thread.Sleep(5);
                if (--nTry == 0)
                {
                    break;
                }
            }
            if (nTry > 0)
            {
                res = 0;
            }
            return res;
        }

        public void SendStringToTCP(NetworkStream stream, ref String s_tx, ref String s_rx, int timeout = 150/*miliseconds*/)
        {
            if ((s_rx == null) || (s_tx == null) || (stream == null)) { return; }
            int index = 0;
            int tcp_rx_bytes = 0;
            s_rx = String.Empty;

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(s_tx);    //send console command
            try
            {
                stream.Write(data, 0, data.Length);                     //to TCP stream
            }

            catch (IOException e7)
            {
                SetDiagText("Socket is busy. IO exception" + e7);
            }
            catch (ArgumentNullException e8)
            {
                SetDiagText("Socket is busy. Argument null" + e8);
            }
            catch (ArgumentOutOfRangeException e9)
            {
                SetDiagText("Socket is busy. Argument out of range" + e9);
            }
            catch
            {

                SetDiagText("Server is busy");
            }
            if (timeout > 0)                                            //if response is expected timeout is non-zero
            {
                Thread.Sleep(timeout);
                if (WaitForResponseFromTCP(stream) == 0)      //if there is some response
                {
                    do
                    {
                        tcp_rx_bytes += tcp_stream.Read(rxbuffer, index, rxbuffer.Length);
                        index = tcp_rx_bytes;
                        if (tcp_rx_bytes >= rxbuffer.Length) break;
                    }
                    while (tcp_stream.DataAvailable);
                    s_rx = System.Text.Encoding.ASCII.GetString(rxbuffer, 0, tcp_rx_bytes); //convert response to text
                }
                else
                {
                    SetDiagText("No response to Energyscan command");
                }
            }
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
           // instance the event args and pass it each value
            ScanFormCloseEventArgs args = new ScanFormCloseEventArgs(0x01);
           // raise the event with the updated arguments
            ScanFormClosing(this, args);
            if(net_conn.GetTcpConnected())
            {
                if(net_conn.IsMyConnection(Form3NeworkConnectionOwner))
                {
                    net_conn.CloseConnection(Form3NeworkConnectionOwner);
                }
            }
        }

        private void RFChannelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((RFChannelBox.Text.Equals("All")) || (RFChannelBox.Text.Equals("ALL")))
            {
                CurrentRFchannel = 0;
            }
            else
            {
                int index = "Channel".Length;
                String chnum_str = RFChannelBox.Text;
                if (chnum_str.Length > index)
                {
                    chnum_str = RFChannelBox.Text.Substring(index);
                    CurrentRFchannel = Convert.ToUInt32(chnum_str);
                }
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            if (tcp_connected)
            {
                for (int i = 0; i < 16; i++)
                {
                    rfdata[i].SetAverage(-89);
                    rfdata[i].SetPeak(-89);
                    rfdata[i].SetChannel(i + (int)(rfchannels.CH11));
                }
                ChartUpdate();
                this.backgroundWorker2.RunWorkerAsync();
                ScanButton.Enabled = false;
                ann.Visible = true;
            }
            else
            {
                SetHostNameText("TCP connection is not established");
            }
 
        }
     }
    public class ScanFormCloseEventArgs : System.EventArgs
    {
        private int mclosed;
        // class constructor
        public ScanFormCloseEventArgs(int sclosed)
        {
            this.mclosed = sclosed;
        }
        public int Closed
        {
            get { return mclosed; }
        }
    }
}
