using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Dapper;

namespace Smartflow
{
    public class WorkflowTransition : Transition
    {
        public Transition GetTransition(string NID)
        {
            return Connection.Query<Transition>(ResourceManage.
                GetString(ResourceManage.SQL_WORKFLOW_TRANSITION_RECORD),
                new
                {
                    NID = NID
                }).FirstOrDefault();
        }
    }
}
