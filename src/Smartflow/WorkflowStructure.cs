using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public class WorkflowStructure
    {
        public string NID
        {
            get;
            set;
        }

        public string StructName
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

        public string StructXml
        {
            get;
            set;
        }

        public string Memo
        {
            get;
            set;
        }

        public int Status
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
