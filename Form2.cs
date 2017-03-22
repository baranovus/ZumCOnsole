using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZumConsole
{
    public partial class Form2 : Form
    {
 
        /***************** Create event of OK button click **********/
        public delegate void NetUpdateHandler(object sender, NetUpdateEventArgs e);
        public event NetUpdateHandler NetParametersUpdated;        // add an event of the delegate type
        /****************************************************************/

        /***************** Create event of form closing**********/
         public delegate void ConnFormClosingHandler(object sender, ConnFormCloseEventArgs e);
         public event ConnFormClosingHandler ConnFormClosing;       // add an event of the delegate type on form close
        /***********************************************************/        
        
        int PortNumber = 4900;
        String Hostname = String.Empty;

        delegate void SetTextCallback(string text);
        
        public Form2()
        {

            InitializeComponent();
            PortNumber = ZumConsole.Properties.Settings.Default.Port;
            PortName.Text = "" + PortNumber;
            HostNameBox.Text = ZumConsole.Properties.Settings.Default.Hostname;


        }

        private void SetDiagText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true.
            if (this.ConnectingLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDiagText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.ConnectingLabel.Text = text;
            }
        }
        
        private void Set_Click(object sender, EventArgs e)
        {
            string sNewHost = HostNameBox.Text;
            string sNewPort = PortName.Text;
            Int32 sPort_num = 0;
            ConnectingLabel.Text = "Connecting...";
            Application.DoEvents();
            try {
                sPort_num = Convert.ToInt32(sNewPort);
            }
            catch (FormatException e1)
            {
                sNewPort = "0";
                sPort_num = 0;
            }

            ZumConsole.Properties.Settings.Default.Hostname = HostNameBox.Text;
            ZumConsole.Properties.Settings.Default.Port = sPort_num;
            ZumConsole.Properties.Settings.Default.Save();

            /******Raise the event when OK button is pressed**********/
             NetUpdateEventArgs args = new NetUpdateEventArgs(sNewHost, sNewPort);
            NetParametersUpdated(this, args);
            /****************************************************/

            /******Raise the event of Form disposal **********/
            ConnFormCloseEventArgs args1 = new ConnFormCloseEventArgs(0x01);
            ConnFormClosing(this, args1);
             /****************************************************/

            this.Dispose();


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ZumConsole.Properties.Settings.Default.Hostname = HostNameBox.Text;
            ZumConsole.Properties.Settings.Default.Port = PortNumber;
            ZumConsole.Properties.Settings.Default.Save();
 
            /******Raise the event when Form is closed **********/
            ConnFormCloseEventArgs args1 = new ConnFormCloseEventArgs(0x01);
             ConnFormClosing(this, args1);
            /****************************************************/
        }
    }

    public class ConnFormCloseEventArgs : System.EventArgs
    {
        private int mclosed;
        // class constructor
        public ConnFormCloseEventArgs(int sclosed)
        {
            this.mclosed = sclosed;
        }
        public int Closed
        {
            get { return mclosed; }
        }
    }
    
    public class NetUpdateEventArgs : System.EventArgs
    {
        // add local member variable to hold text
        private string mHostname;
        private string mPortname;
 

        // class constructor
        public NetUpdateEventArgs(string sHostname, string sPortname)
        {
            this.mHostname = sHostname;
            this.mPortname = sPortname;
 
        }

        // Properties - Accessible by the listener

        public string HostName
        {
            get
            {
                return mHostname;
            }
        }

        public string PortName
        {
            get
            {
                return mPortname;
            }
        }
    }

  
 
}
