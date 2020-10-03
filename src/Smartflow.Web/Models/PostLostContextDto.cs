using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PostLostContextDto
    {
        [Required]
        [StringLength(50)]
        public string ID
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string Destination
        {
            get;
            set;
        }

        [StringLength(1024)]
        public string Message
        {
            get;
            set;
        }


        public dynamic Data
        {
            get;
            set;
        }
    }
}