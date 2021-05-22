using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class CreatePending: ICommand
    {
        public void Execute(Object o)
        {
            using ISession session = DbFactory.OpenSession();
            session.Save(o);
            session.Flush();
        }
    }
}
