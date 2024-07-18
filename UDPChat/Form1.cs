using System.Net.Sockets;
using System.Text;

namespace UDPChat
{
    public partial class Form1 : Form
    {

        Server server;
        public Form1()
        {
            InitializeComponent();
            server = new Server("224.0.0.123", 17234);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            server.Message = textBox1.Text;
        }
       






    }
}
