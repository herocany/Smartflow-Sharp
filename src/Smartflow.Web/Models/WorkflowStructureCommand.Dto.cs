using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class WorkflowStructureCommandDto
    {
        [StringLength(50)]
        public string NID
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string StructName
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string CateCode
        {
            get;
            set;
        }
        [Required]
        [StringLength(50)]
        public string CateName
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Text)]
        public string StructXml
        {
            get;
            set;
        }

        [StringLength(2048)]
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
    }
}