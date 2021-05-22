using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePending : ICommand
    {
        public void Execute(object o)
        {
            using ISession session = DbFactory.OpenSession();
            session
                .CreateQuery("delete from Pending p where p.InstanceID=:InstanceID")
                .SetParameter("InstanceID", o)
                .ExecuteUpdate();

            session.Flush();
        }
    }
}
