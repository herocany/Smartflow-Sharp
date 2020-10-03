using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PendingCommandDto
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
        public string CateCode
        {
            get;
            set;
        }

        [Required]
        [StringLength(1024)]
        public string ActorIDs
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string NodeID
        {
            get;
            set;
        }
    }
}