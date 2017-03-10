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
        // add a delegate
        /***************** Create event of OK button click **********/
        public delegate void NetUpdateHandler(object sender, NetUpdateEventArgs e);

        // add an event of the delegate type
        public event NetUpdateHandler NetParametersUpdated;


        /***************** Create event of form closing**********/
        // add a delegate
        public delegate void ConnFormClosingHandler(object sender, ConnFormCloseEventArgs e);
        // add an event of the delegate type on form close
        public event ConnFormClosingHandler ConnFormClosing;
        /***********************************************************/        
        
        int PortNumber = 4900;
        String Hostname = String.Empty; 
 

        
        public Form2()
        {

            InitializeComponent();
            PortNumber = ZumConsole.Properties.Settings.Default.Port;
            PortName.Text = "" + PortNumber;
            HostName.Text = ZumConsole.Properties.Settings.Default.Hostname;
        }

        private void Set_Click(object sender, EventArgs e)
        {
            string sNewHost = HostName.Text;
            string sNewPort = PortName.Text;
  

            // instance the event args and pass it each value
            NetUpdateEventArgs args = new NetUpdateEventArgs(sNewHost,
                sNewPort);

            // raise the event with the updated arguments
            NetParametersUpdated(this, args);
            // instance the event args and pass it each value
            ConnFormCloseEventArgs args1 = new ConnFormCloseEventArgs(0x01);
            // raise the event with the updated arguments
            ConnFormClosing(this, args1);

            this.Dispose();


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // instance the event args and pass it each value
            ConnFormCloseEventArgs args1 = new ConnFormCloseEventArgs(0x01);
            // raise the event with the updated arguments
            ConnFormClosing(this, args1);
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
