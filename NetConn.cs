using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using ZumConsole.Properties;


namespace ZumConsole
{
    public class NetworkConn
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool tcp_connected;
        private String Hostname;
        private Int32 Port;
        private Int32 ConnectionOwner;
        public NetworkConn()
        {
            tcp_connected = false;
            Hostname = String.Empty;
            Port = 0;
            ConnectionOwner = 0;
        }
        public String GetHostName() { return Hostname; }
        public Int32 GetPort() { return Port; }
        public Int32 GetOwner() { return ConnectionOwner; }
        public bool GetTcpConnected() { return tcp_connected; }
        public NetworkStream GetStream() { return stream;}

        private bool IsValidIp(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }
        public NetworkStream MakeConnection(String Hostname, Int32 Port, Int32 Owner = 1)
        {
            if ((String.IsNullOrEmpty(Hostname)) || (Port == 0))
            {
                return null;
            }
            try
            {
                if (IsValidIp(Hostname))
                {
                    IPAddress ip_address = IPAddress.Parse(Hostname);
                    IPEndPoint ipLocalEndPoint = new IPEndPoint(ip_address, Port);
                    client = new TcpClient();
                    client.Connect(ipLocalEndPoint);
                    if (client.Connected) { tcp_connected = true; }

                }
                else
                {
                    IPHostEntry hostInfo = Dns.GetHostEntry(Hostname);
                    client = new TcpClient(hostInfo.HostName, Port);
                    if (client.Connected) { tcp_connected = true; }
                }

            }
            catch (SocketException e4)
            {
                tcp_connected = false;
            }
            if (tcp_connected)
            {
                try
                {
                    stream = client.GetStream();
                    tcp_connected = true;
                    ZumConsole.Properties.Settings.Default.Hostname = Hostname;
                    ZumConsole.Properties.Settings.Default.Port = Port;
                    ZumConsole.Properties.Settings.Default.Save();
                    ConnectionOwner = Owner;
                }
                catch (SocketException e5)
                {
                    tcp_connected = false;
                }
            }
            else { stream = null; }
            return (stream);
        }
        public void CloseConnection(Int32 Owner = 1)
        {
            if (Owner == ConnectionOwner)
            {
                if ((client != null) && (client.Connected))
                {
                    client.Close();
                    ConnectionOwner = 0;
                    tcp_connected = false;
                }
            }
        }
    }
}
