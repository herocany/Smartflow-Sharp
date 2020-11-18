using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class WorkflowDeleteDto
    {
        [Required]
        [StringLength(50)]
        public string InstanceID
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string Key
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
    }
}