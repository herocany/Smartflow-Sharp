using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartflow.Web.Models
{
    public class WorkflowStructureDto
    {
        public virtual string NID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string CategoryCode
        {
            get;
            set;
        }

        public virtual string CategoryName
        {
            get;
            set;
        }

        public virtual string Resource
        {
            get;
            set;
        }

        public virtual int Status
        {
            get;
            set;
        }

        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        public virtual string Memo
        {
            get;
            set;
        }
    }
}
