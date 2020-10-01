using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class SummaryDeleteDto
    {
        [Required]
        public string InstanceID
        {
            get;
            set;
        }

        [Required]
        public string Key
        {
            get;
            set;
        }

        [Required]
        public string CategoryID
        {
            get;
            set;
        }
    }
}