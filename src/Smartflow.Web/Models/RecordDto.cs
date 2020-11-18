using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class RecordDto
    {
        public string NID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public string InstanceID
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string AuditUserID
        {
            get;
            set;
        }

        public string AuditUserName
        {
            get;
            set;
        }

        public string NodeName
        {
            get;
            set;
        }

        public string NodeID
        {
            get;
            set;
        }
    }
}