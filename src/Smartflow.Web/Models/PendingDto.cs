using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PendingDto
    {
        public string ID
        {
            get;
            set;
        }

        public string CategoryCode
        {
            get;
            set;
        }

        public string ActorIDs
        {
            get;
            set;
        }

        public string NodeID
        {
            get;
            set;
        }
    }
}