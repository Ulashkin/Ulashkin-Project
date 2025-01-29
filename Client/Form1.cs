using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            _ = Listener();

        }





        async Task Listener()
        {
            var multicastAddress = IPAddress.Parse("224.0.0.123");
            int port = 17234;
            using var client = new UdpClient();
            client.JoinMulticastGroup(multicastAddress);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.Client.Bind(new IPEndPoint(IPAddress.Any, port));

            while (true)
            {
                var res = await client.ReceiveAsync();
                var msg = Encoding.UTF8.GetString(res.Buffer);
                textBox2.Invoke(() =>
                {
                    textBox2.Text = msg;
                });
            }
        }




    }
}