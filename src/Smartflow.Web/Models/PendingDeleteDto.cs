using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PendingDeleteDto
    {
        public string ID
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