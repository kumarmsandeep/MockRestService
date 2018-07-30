using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace MockRestService.Server
{
    public class SimpleHTTPServer
    {
        
        private Thread serverThread;
        private HttpListener listener;
        private int port;

        public delegate void RequestHandler(HttpListenerContext context);

        public event RequestHandler OnRequestRecieved;
        
        public int Port
        {
            get { return port; }
            private set { }
        }

        public SimpleHTTPServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            serverThread = new Thread(this.Listen);
            serverThread.Start();
        }

        public void Stop()
        {
            serverThread.Abort();
            listener.Stop();
        }

        private void Listen()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:" + port.ToString() + "/");
            listener.Start();
            while (true)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    Process(context);
                }
                catch (Exception ex)
                {
                    Console.Write(ex + "");
                }
            }
        }

        private void Process(HttpListenerContext context)
        {
            if (OnRequestRecieved != null)
            {
                OnRequestRecieved.Invoke(context);
            }
            context.Response.OutputStream.Close();
        }
    }
}
