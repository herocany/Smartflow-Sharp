using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartflow.Web.Models
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
