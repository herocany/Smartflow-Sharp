/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System.ComponentModel.DataAnnotations;

namespace Smartflow.Abstraction.Body
{
    /// <summary>
    /// 桥接实
    /// </summary>
    public class BridgeBody
    {
        [Required]
        [StringLength(50)]
        public string CategoryCode
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

        [StringLength(2048)]
        public string Comment
        {
            get; set;
        }

        [Required]
        [StringLength(50)]
        public string Creator
        {
            get;
            set;
        }
    }
}