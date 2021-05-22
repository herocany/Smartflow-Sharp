using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Core
{
    public class WorkflowLink
    {
        public virtual string NID
        {
            get;
            set;
        }

        public virtual string InstanceID
        {
            get;
            set;
        }

        public virtual string RelationshipID
        {
            get;
            set;
        }
    }
}
