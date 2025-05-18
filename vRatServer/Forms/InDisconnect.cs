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
    public partial class OnDisconnect : Form
    {
        Client cli;
        byte[] r = null;
        public OnDisconnect(Client cli)
        {
            this.cli = cli;
            InitializeComponent();
        }

        public void updatecmd(string clientcmd)
        {
            textBox1.Text = clientcmd;
        }
        void setcmd(string cmd)
        {
            byte[] commandUni = System.Text.Encoding.Unicode.GetBytes(cmd).Concat(new byte[2]).ToArray();

            globals.SendPacket(cli, (byte)globals.PacketType.setondis, commandUni.Length, 0, ref commandUni);
        }
        private void OnDisconnect_Load(object sender, EventArgs e)
        {

            cli.ondsc = this;
            this.Text = this.Text + "  - " + cli.Name;
            globals.SendPacket(cli, (byte)globals.PacketType.getondis, 0, 0, ref r);
            this.ActiveControl = label1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setcmd(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setcmd("");
        }

        private void OnDisconnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            cli.ondsc = null;
        }
    }
}
