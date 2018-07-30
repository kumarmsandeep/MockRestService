using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public class WorkSpace
    {
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private TargetHost targetHost;

        public TargetHost TargetHost
        {
            get { return targetHost; }
            set { targetHost = value; }
        }
        private Dictionary<String, APICategory> categories = new Dictionary<string, APICategory>();

        public Dictionary<String, APICategory> Categories
        {
            get { return categories; }
            set { categories = value; }
        }
        private Dictionary<String, RestService> services = new Dictionary<string, RestService>();

        public Dictionary<String, RestService> Services
        {
            get { return services; }
            set { services = value; }
        }
        private Dictionary<String, RestAPI> apis = new Dictionary<string, RestAPI>();

        public Dictionary<String, RestAPI> Apis
        {
            get { return apis; }
            set { apis = value; }
        }


    }
}
