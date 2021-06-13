/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */

using Smartflow.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core.Components
{
    public class DefaultStrategyService : IStrategyService
    {
        public bool Check(IList<WorkflowCooperation> records)
        {
            return (records.Count >= 3);
        }
    }
}
