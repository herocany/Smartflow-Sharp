using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class UpdateBridge : ICommand
    {
        public void Execute(Object o)
        {
            Bridge model = (o as Bridge);
            using ISession session = DbFactory.OpenSession();
            string hql = " update Bridge b set b.InstanceID =:InstanceID where b.Key =:Key ";
            session.CreateQuery(hql)
                .SetParameter("InstanceID", model.InstanceID)
                .SetParameter("Key", model.Key)
               .ExecuteUpdate();
        }
    }
}
