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
        public string Name
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string CategoryCode
        {
            get;
            set;
        }
        [Required]
        [StringLength(50)]
        public string CategoryName
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Text)]
        public string Resource
        {
            get;
            set;
        }


        public int Status
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
    }
}