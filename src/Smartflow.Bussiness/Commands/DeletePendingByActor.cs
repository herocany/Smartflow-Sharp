using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePendingByActor : ICommand
    {
        public void Execute(Object o)
        {
            Dictionary<string, object> queryArg = (o as Dictionary<string, object>);
            using ISession session = DbFactory.OpenSession();
            session
                .CreateQuery(" delete from Pending p where p.InstanceID=:InstanceID and p.NodeID=:NodeID and p.ActorID=:ActorID ")
                .SetParameter("InstanceID", queryArg["instanceID"])
                .SetParameter("NodeID", queryArg["nodeID"])
                .SetParameter("ActorID", queryArg["actorID"])
                .ExecuteUpdate();
            session.Flush();
        }
    }
}
