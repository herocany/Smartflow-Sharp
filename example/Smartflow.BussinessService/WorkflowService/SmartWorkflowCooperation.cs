using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Elements;

namespace Smartflow.BussinessService.WorkflowService
{
    public class SmartWorkflowCooperation : AbstractWorkflowCooperation
    {
        public override IWorkflowCooperationStrategy SelectStrategy()
        {
            return new LastStrategy();
        }

        public override bool Check(ASTNode node, IList<WorkflowProcess> records)
        {
            if (node.Cooperation > 0)
            {
                //两个人审批的时候
                return records.Count >= 2;
            }
            else
            {
                return true;
            }
        }
    }
}
