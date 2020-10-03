using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class WorkflowStructureRequstDto
    {
        [Required]
        [StringLength(50)]
        public string NID
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