using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class CreateRecord : ICommand
    {
        public void Execute(Object o)
        {
            using ISession session = DbFactory.OpenSession();
            session.Save(o);
            session.Flush();
        }
    }
}
