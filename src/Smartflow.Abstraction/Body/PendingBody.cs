/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System.ComponentModel.DataAnnotations;

namespace Smartflow.Abstraction.Body
{
    public class PendingBody
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
        public string CategoryCode
        {
            get;
            set;
        }

        [Required]
        [StringLength(2048)]
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