using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRestService.Model
{
    public class MockObject
    {
        private String id;
        private String name;
        private String description;

        public String Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public String Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
