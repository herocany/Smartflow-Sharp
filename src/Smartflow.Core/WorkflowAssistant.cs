/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Core
{
    public class WorkflowAssistant
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

        public virtual string NodeID
        {
            get;
            set;
        }

        public virtual int Total
        {
            get;
            set;
        }
    }
}
