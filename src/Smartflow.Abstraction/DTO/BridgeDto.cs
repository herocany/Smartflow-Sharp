/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;

namespace Smartflow.Abstraction.DTO
{
    public class BridgeDto
    {
        public string InstanceID
        {
            get;
            set;
        }

        public string CategoryCode
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Comment
        {
            get; set;
        }

        public string Creator
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}