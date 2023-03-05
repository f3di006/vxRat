using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace vRatServer.Classes
{
        class sysinfo
        {
            byte[] packet;
            Client client;
            byte member;
            void Screens()
            {

            }

            void set_client(string k)
            {



                StringReader reader = new StringReader(k);
                int i = 0;
                while (true)
                {
                    string line = reader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }
                    line = line.TrimEnd('\r', '\n');
                    if (i == 0) { client.Name = line; }
                    else if (i == 1) { client.gpu = line; }
                    else if (i == 2) { client.cpu_arch = line; }
                    else if (i == 3) { client.WindowsVers = line; }
                    else if (i == 4) { client.privilege = line; }
                    else { client.screens.Add(line); }
                    i++;

                }

                client.add_client();


            }

            public sysinfo(byte[] packet, Client cli, byte member)
            {
                this.member = member;
                this.packet = packet;
                client = cli;
                set_client(System.Text.Encoding.Unicode.GetString(packet));

            }
        }
    }
