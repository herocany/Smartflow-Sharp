using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Models
{
    public class Organization
    {
        public string ID
        {
            get;
            set;
        }

        public string ParentID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
