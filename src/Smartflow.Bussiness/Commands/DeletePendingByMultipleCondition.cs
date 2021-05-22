using NHibernate;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePendingByMultipleCondition : ICommand
    {
        public void Execute(object o)
        {
            Dictionary<string, object> queryArg = (o as Dictionary<string, object>);

            using ISession session = DbFactory.OpenSession();
            session
                .CreateQuery("delete from Pending p where p.InstanceID=:InstanceID and p.NodeID=:NodeID")
                .SetParameter("InstanceID", queryArg["instanceID"])
                .SetParameter("NodeID", queryArg["nodeID"])
                .ExecuteUpdate();

            session.Flush();
        }
    }
}
