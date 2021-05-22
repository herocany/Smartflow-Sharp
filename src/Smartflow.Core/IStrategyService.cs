using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public interface IStrategyService
    {
        bool Check(IList<WorkflowCooperation> records);
    }
}
