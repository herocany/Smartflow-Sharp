using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Models
{
    public class Pending
    {
        public string NID
        {
            get;
            set;
        }

        public string ActorID
        {
            get;
            set;
        }

        public string NodeID
        {
            get;
            set;
        }

        public string InstanceID
        {
            get;
            set;
        }

        public string NodeName
        {
            get;
            set;
        }

        public string CateCode
        {
            get;
            set;
        }

        public string CateName
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public DateTime CreateDateTime
        {
            get;
            set;
        }
    }
}
