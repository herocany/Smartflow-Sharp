/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System.ComponentModel.DataAnnotations;

namespace Smartflow.Abstraction.Body
{
    public class WorkflowStructureBody
    {
        [Key]
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

        [Required]
        [StringLength(50)]
        public string Memo
        {
            get;
            set;
        }
    }
}
