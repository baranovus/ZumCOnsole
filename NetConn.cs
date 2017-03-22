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
 /* Singleton class for network connection. It is used for connecting different forms to Control System TCP/IP console.
  It is said that Control system console supports a limited (up to 5) number of console connections, thus forms
  will share one connection. They will not be able to make transactions simultaneously.
  */
    public sealed class NetworkConn : IDisposable
    {
        private static NetworkConn instance = null;
        private static readonly object padlock = new object();
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
            ConnectionOwner = -1;
        }
        public static NetworkConn Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NetworkConn();
                    }
                    return instance;
                }
            }
        }
        
        
        public String GetHostName() { return Hostname; }
        public Int32 GetPort() { return Port; }
        public Int32 GetOwner() { return ConnectionOwner; }
        public bool GetTcpConnected() { return tcp_connected; }
        public NetworkStream GetStream() { return stream;}
        public bool IsMyConnection(Int32 owner)
        {
            return (owner == ConnectionOwner);
        }
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
                    ConnectionOwner = Owner;
                    this.Hostname = Hostname;
                    this.Port = Port;
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
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (client != null)
                {
                    client.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }

            }

        }

        public void Dispose()
        {
            Dispose(true);

        }

    }
}
