using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PostContextDto
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
        public String ActorID
        {
            get;
            set;
        }

        [StringLength(2048)]
        public string Message
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public String TransitionID
        {
            get;
            set;
        }

        [Required]
        public dynamic Data
        {
            get;
            set;
        }
    }
}