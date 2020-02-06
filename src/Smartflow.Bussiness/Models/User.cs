using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Models
{
    public class User
    {
        public string UniqueId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string OrgCode
        {
            get;
            set;
        }
    }
}
