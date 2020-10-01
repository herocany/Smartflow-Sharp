using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public interface IJump
    {
        void Jump(WorkflowContext context);

        void Jump(string to, WorkflowContext context);
    }
}
