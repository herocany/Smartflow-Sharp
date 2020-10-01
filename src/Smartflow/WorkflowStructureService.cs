using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using Dapper;
namespace Smartflow
{
    public class WorkflowStructureService : WorkflowInfrastructure, IWorkflowPersistent<WorkflowStructure>, IWorkflowQuery<IList<WorkflowStructure>, string>, IWorkflowQuery<IList<WorkflowStructure>>
    {
        public void Persistent(WorkflowStructure entry)
        {
            string cmd = string.IsNullOrEmpty(entry.NID) ? ResourceManage.SQL_WORKFLOW_STRUCTURE_INSERT : ResourceManage.SQL_WORKFLOW_STRUCTURE_UPDATE;
            base.Connection.Execute(cmd,entry);
        }

        public void Delete(string id)
        {
            base.Connection.Execute(ResourceManage.SQL_WORKFLOW_STRUCTURE_DELETE, new { NID = id });
        }

        public IList<WorkflowStructure> Query(string id)
        {
            return base.Connection.Query<WorkflowStructure>(ResourceManage.SQL_WORKFLOW_STRUCTURE_SELECT_ID, new { NID = id }).ToList();
        }

        public IList<WorkflowStructure> Query()
        {
            return base.Connection.Query<WorkflowStructure>(ResourceManage.SQL_WORKFLOW_STRUCTURE_SELECT).ToList();
        }

        public IList<WorkflowStructure> Query(int pageIndex, int pageSize, out int total, Dictionary<string, string> queryArg)
        {
            string conditionStr = SetQueryArg(queryArg);
            string query = String.Format(ResourceManage.SQL_WORKFLOW_STRUCTURE_SELECT_PAGING, pageSize, pageSize * (pageIndex - 1), conditionStr);
            total = base.Connection.ExecuteScalar<int>(String.Format(ResourceManage.SQL_WORKFLOW_STRUCTURE_SELECT_TOTAL, conditionStr));
            return base.Connection.Query<WorkflowStructure>(query).ToList();
        }

        private string SetQueryArg(Dictionary<string, string> queryArg)
        {
            StringBuilder buildWhere = new StringBuilder();

            if (queryArg.ContainsKey("CateCode"))
            {
                buildWhere.AppendFormat(" And CateCode='{0}'", queryArg["CateCode"]);
            }

            if (queryArg.ContainsKey("key"))
            {
                buildWhere.AppendFormat(" And StructName LIKE '%{0}%'", queryArg["key"]);
            }

            return buildWhere.ToString();
        }
    }
}
