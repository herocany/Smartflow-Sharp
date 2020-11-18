using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class WorkflowStructureDto
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

        public string CategoryCode
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        public string Resource
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public string Memo
        {
            get;
            set;
        }
    }
}