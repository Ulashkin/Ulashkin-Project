using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPChat
{
    internal class Server
    {
       
        private IPEndPoint serverEndPoint;
        private IPEndPoint clientEndPoint;
        private string username;
        private readonly string multicastAddress;
        private readonly int multicastPort;
        private readonly UdpClient udpClient;
        private string message = "Hello";


        public string Message
        {
            set => message = value;
            get => message;
        }




        public Server(string multicastAddress, int multicastPort)
        {
            this.multicastAddress = multicastAddress;
            this.multicastPort = multicastPort;
            udpClient = new UdpClient();
            udpClient.JoinMulticastGroup(IPAddress.Parse(multicastAddress), 1);
            _ = SendMessageAsync();

        }
        private async Task SendMessageAsync()
        {
            while (true)
            {
                var msg = Encoding.UTF8.GetBytes(message);
                var endPoint = new IPEndPoint(IPAddress.Parse(multicastAddress), multicastPort);
                await udpClient.SendAsync(msg, msg.Length, endPoint);
                await Task.Delay(1000);
            }
        }

        private void ReleaseResouces()
        {
            udpClient.Dispose();
        }

        public void Dispose()
        {
            ReleaseResouces();
            GC.SuppressFinalize(this);
        }
        ~Server()
        {
            ReleaseResouces();
        }




        private void ReceiveMessages()
        {
            udpClient.Client.Bind(clientEndPoint);
            while (true)
            {
                byte[] receivedData = udpClient.Receive(ref clientEndPoint);
                string message = Encoding.ASCII.GetString(receivedData);
                //AddMessageToHistory(message);
            }
        }




        //private void AddMessageToHistory(string message)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new MethodInvoker(delegate { AddMessageToHistory(message); }));
        //    }
        //    else
        //    {
        //        lstHistory.Items.Add(message);
        //    }
        //}


    }
}
