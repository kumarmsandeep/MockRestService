using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockRestService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Server.SimpleHTTPServer server = new Server.SimpleHTTPServer(8087);
            Model.WorkSpace workspace = new Model.WorkSpace();
            workspace.TargetHost = new Model.TargetHost();
            workspace.TargetHost.Host = "greatandhra.com";
            workspace.TargetHost.Protocol = "https";
            Server.HTTPRequestHandler handler = new Server.HTTPRequestHandler(server, workspace);
            handler.StartHandling();
            server.Start();
            Application.Run(new Form1());
        }
    }
}
