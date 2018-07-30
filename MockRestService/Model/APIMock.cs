using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public class APIMock : MockObject
    {
        private List<MockRule> rules = new List<MockRule>();
        private ResponseInfo responseInfo;
        private RequestProcessingMode requestProcessingMode;

        public List<MockRule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
        
        public RequestProcessingMode RequestProcessingMode
        {
            get { return requestProcessingMode; }
            set { requestProcessingMode = value; }
        }
        
        public ResponseInfo ResponseInfo
        {
            get { return responseInfo; }
            set { responseInfo = value; }
        }

    }
}
