/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 ********************************************************************
 */
using System;

namespace Smartflow.Bussiness.Models
{
    public class Category
    {
        public virtual string NID
        {
            get;
            set;
        }

        public virtual string ParentID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Url
        {
            get;
            set;
        }
    }
}
