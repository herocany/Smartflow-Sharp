using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smartflow.Web.Models
{
    public class WorkflowStructureCommandDto
    {
        [Key]
        public virtual string NID
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public virtual string Name
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public virtual string CategoryCode
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public virtual string CategoryName
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Text)]
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

        [Required]
        [StringLength(50)]
        public virtual string Memo
        {
            get;
            set;
        }
    }
}
