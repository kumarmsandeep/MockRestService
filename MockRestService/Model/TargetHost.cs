using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public class TargetHost
    {
        private String host;

        public String Host
        {
            get { return host; }
            set { host = value; }
        }
        private String protocol;

        public String Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }
        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}
