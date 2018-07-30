using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public class RestAPI : MockObject
    {
        private HttpMethod method;

        public HttpMethod Method
        {
            get { return method; }
            set { method = value; }
        }
        private String path;

        public String Path
        {
            get { return path; }
            set { path = value; }
        }
        private String restServiceID;

        public String RestServiceID
        {
            get { return restServiceID; }
            set { restServiceID = value; }
        }
        private List<APIMock> apiMocks;

        public List<APIMock> ApiMocks
        {
            get { return apiMocks; }
            set { apiMocks = value; }
        }
    }
}
