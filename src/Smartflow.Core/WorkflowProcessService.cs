/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using NHibernate;
using Smartflow.Common;
using Smartflow.Core.Internals;
namespace Smartflow.Core
{
    public class WorkflowProcessService : IWorkflowProcessService, IWorkflowPersistent<WorkflowProcess, Action<object>>, IWorkflowQuery<IList<WorkflowProcess>, Dictionary<string, object>>
    {
        public IList<WorkflowProcess> Get(string instanceID)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<WorkflowProcess>()
                          .Where(e => e.InstanceID == instanceID).OrderBy(e => e.CreateTime)
                          .ToList();
        }

        public void Persistent(WorkflowProcess process, Action<object> callback)
        {
            callback(process);
        }

        public IList<WorkflowProcess> Query(Dictionary<string, object> queryArg)
        {
            using ISession session = DbFactory.OpenSession();
            WorkflowOpertaion direction = (WorkflowOpertaion)Enum.Parse(typeof(WorkflowOpertaion),
                queryArg["Direction"].ToString());

            return session.Query<WorkflowProcess>()
                          .Where(e => e.InstanceID == queryArg["InstanceID"].ToString())
                          .Where(e => e.Direction == direction)
                          .OrderByDescending(e => e.CreateTime)
                          .ToList();
        }

        public dynamic Query(string instanceID)
        {
            using ISession session = DbFactory.OpenSession();

            DbCommand command = session.Connection.CreateCommand();
            command.CommandText = ResourceManage.SQL_WORKFLOW_PROCESS_RECORD;
            command.CommandType = CommandType.Text;

            DbParameter instanceIDParameter = command.CreateParameter();
            instanceIDParameter.ParameterName = "InstanceID";
            instanceIDParameter.Value = instanceID;
            instanceIDParameter.DbType = DbType.String;
            instanceIDParameter.Size = 50;
            instanceIDParameter.Direction = ParameterDirection.Input;

            DbParameter directionParameter = command.CreateParameter();
            directionParameter.ParameterName = "Direction";
            directionParameter.Value = WorkflowOpertaion.Go;
            directionParameter.DbType = DbType.String;
            directionParameter.Size = 50;
            directionParameter.Direction = ParameterDirection.Input;

            command.Parameters.Add(instanceIDParameter);
            command.Parameters.Add(directionParameter);

            using IDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
    }
}
