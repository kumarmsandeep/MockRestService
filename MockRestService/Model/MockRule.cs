using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public abstract class MockRule
    {
        private EqualType equalType;
        private Boolean ignoreCase;
    }
}
