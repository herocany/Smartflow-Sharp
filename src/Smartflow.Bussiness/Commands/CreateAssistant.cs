using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Smartflow.Common;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class CreateAssistant : ICommand
    {
        public void Execute(object o)
        {
            using ISession session = DbFactory.OpenSession();
            session.GetNamedQuery("queryAssistantByInstance")
                    .SetParameter("InstanceID", o.ToString())
                    .ExecuteUpdate();
            session.Flush();
        }
    }
}
