using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockRestService.Model;
using System.Net;
using MockRestService.Utils;

namespace MockRestService.Server
{
    public class HTTPRequestHandler
    {
        private SimpleHTTPServer server;
        private WorkSpaceMgr workspaceMgr;

        public HTTPRequestHandler(SimpleHTTPServer httpServer, WorkSpace workspace)
        {
            this.server = httpServer;
            this.workspaceMgr = new WorkSpaceMgr(workspace);
        }

        public void StartHandling()
        {
            server.OnRequestRecieved += server_OnRequestRecieved;
        }

        public void StopHandling()
        {
            server.OnRequestRecieved -= server_OnRequestRecieved;
        }

        private void server_OnRequestRecieved(HttpListenerContext context)
        {

            APIMock mock = workspaceMgr.GetAPIMock(context.Request);

            RequestProcessingMode processingMode = RequestProcessingMode.Proceed;
            
            if(mock != null) 
            {
                processingMode = mock.RequestProcessingMode;
            }

            if (processingMode == RequestProcessingMode.Proceed)
            {
                proceedHTTPCall(context);
            }
            else if (processingMode == RequestProcessingMode.Block)
            {

            }
        }

        private void proceedHTTPCall(HttpListenerContext context)
        {

            try
            {
                WebRequest req = workspaceMgr.getTargetWebRequest(context);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                
                HttpListenerResponse response = context.Response;                
                response.ContentType = res.ContentType;
                response.Cookies = res.Cookies;
                if (response.Headers == null)
                {
                    response.Headers = new WebHeaderCollection();
                }
                foreach (String name in res.Headers.Keys)
                {
                    if (WebHeaderCollection.IsRestricted(name))
                    {
                        continue;
                    }
                    response.Headers.Add(name, res.Headers[name]);
                }
                //response.Headers = res.Headers;
                response.StatusCode = (int)res.StatusCode;
                response.StatusDescription = res.StatusDescription;
                int count = 0;
                int index = 0;
                byte[] buff = new byte[1024];
                while ((count = res.GetResponseStream().Read(buff, 0, buff.Length)) > 0)
                {
                    response.OutputStream.Write(buff, 0, count);
                    index = index + count;
                }
                
            }
            catch (Exception e)
            {

            }
            
        }
    }
}
