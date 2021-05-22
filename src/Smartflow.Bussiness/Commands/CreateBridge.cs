using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using NHibernate;

namespace Smartflow.Bussiness.Commands
{
    public class CreateBridge : ICommand
    {
        public void Execute(Object o)
        {
            using ISession session = DbFactory.OpenSession();
            session.Save(o);
            session.Flush();
        }
    }
}
