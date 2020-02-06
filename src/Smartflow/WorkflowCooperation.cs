using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public class WorkflowCooperation
    {
        public string NID
        {
            get;
            set;
        }

        public string InstanceID
        {
            get;
            set;
        }

        public string TransitionID
        {
            get;
            set;
        }

        public string NodeID
        {
            get;
            set;
        }

        public DateTime CreateDateTime
        {
            get;
            set;
        }
    }
}
