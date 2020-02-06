using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.BussinessService.Models
{
    public class User
    {
        public string IDENTIFICATION
        {
            get;
            set;
        }

        public string USERNAME
        {
            get;
            set;
        }

        public string USERPWD
        {
            get;
            set;
        }
    
        public string ORGCODE
        {
            get;
            set;
        }

        public string ORGNAME
        {
            get;
            set;
        }
    }
}
