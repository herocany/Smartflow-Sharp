using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PostContextDto
    {
        public string InstanceID
        {
            get;
            set;
        }

        public String ActorID
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public String TransitionID
        {
            get;
            set;
        }

        public dynamic Data
        {
            get;
            set;
        }
    }
}