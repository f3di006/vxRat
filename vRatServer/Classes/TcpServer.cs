using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vRatServer.Classes
{
    class TcpServer
    {
        private TcpListener _server;
        private bool _isRunning = true;
        private List<Socket> clients;
        public TcpServer(int port)
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, port);
                _server.Start();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            clients = new List<Socket>();
        }

        public void Start()
        {
            Task.Run(() => Listen());
        }

        private void Listen()
        {
            while (_isRunning)
            {
                Socket client = _server.AcceptSocket();
                clients.Add(client);
                new Thread(new ParameterizedThreadStart(HandleClient)).Start(client);
            }
            
        }
        public void Stop()
        {
            _isRunning = false;
            _server.Stop();
            foreach(Socket s in clients)
            {
                s.Close();
            }
        }
        private void HandleClient(object obj)
        {
            
            Socket client_s = (Socket)obj;
            Client ci = new Client(client_s);
            ci.recv_data();



            return;
        }
    }
}
