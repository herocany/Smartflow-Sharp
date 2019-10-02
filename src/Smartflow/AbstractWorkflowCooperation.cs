using Smartflow.Components;
using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public abstract class AbstractWorkflowCooperation
    {
        public abstract Boolean Check(ASTNode node, IList<WorkflowProcess> records);

        public virtual IWorkflowCooperationStrategy SelectStrategy()
        {
            return new DemocraticStrategy();
        }

        public virtual void Detached(IList<WorkflowProcess> records,string destination,Action<WorkflowProcess> callback,Action<WorkflowProcess> updateCallback)
        {
            var record= records.Where(r => r.Destination == destination).FirstOrDefault();

            if (record != null)
            {
                records.Where(r => r.NID != record.NID).ToList().ForEach(r => callback(r));

                updateCallback(record);
            }
        }
    }
}
