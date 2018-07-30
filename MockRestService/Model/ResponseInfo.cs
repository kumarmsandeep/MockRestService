using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public class ResponseInfo
    {
        private int httpStatus;

        private Dictionary<String, String> headers;

        private String payload;
    }
}
