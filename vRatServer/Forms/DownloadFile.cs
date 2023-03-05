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
    public partial class DownloadFile : Form
    {
        public Mutex DownloadFileFormM;
        public string filename;
        public string filesize;
        public ulong intfilesize;
        public string path;
        public int total_b = 0;
        public static ulong fileid = 0;
        public ulong myfileid;
        public FileStream stream;
        private Client client;
        public bool downloaded = false;
        public DownloadFile(string filename, string filesize, string path, Client client)
        {
            this.filename = filename;
            this.filesize = filesize;
            this.intfilesize = ulong.Parse(this.filesize);
            this.path = path;
            this.client = client;
            try
            {
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            InitializeComponent();
        }
        public void ChangeFileSize(int h)
        {
            ulong curr = ulong.Parse(label3.Text);
            curr += (ulong)h;
            label3.Text = curr.ToString();
            //this.Refresh();
            if (curr >= intfilesize)
            {

                label2.ForeColor = Color.Green;
                label3.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;

            }
        }
        private void DownloadFile_Load(object sender, EventArgs e)
        {

            label4.Text = filesize;
            label5.Text = filename;
        }
        private void Stop()
        {
            byte[] r = null;
            globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileDownloadStop, 0, myfileid, ref r);

        }
        private void DownloadFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stop();
            button1.Enabled = false;
        }
    }
}
