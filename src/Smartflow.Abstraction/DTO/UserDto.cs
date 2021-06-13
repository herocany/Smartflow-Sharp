/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;

namespace Smartflow.Abstraction.DTO
{
    public class UserDto
    {
        public virtual string ID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string OrganizationCode
        {
            get;
            set;
        }

        public virtual string OrganizationName
        {
            get;
            set;
        }
    }
}
