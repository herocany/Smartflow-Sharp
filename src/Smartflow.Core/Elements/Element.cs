/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow.Core.Elements
{
    public abstract class Element 
    {
        protected string name = string.Empty;
        protected string id = string.Empty;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 节点标识ID
        /// </summary>
        public virtual string ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual string NID
        {
            get;
            set;
        }

        public virtual string InstanceID
        {
            get;
            set;
        }
    }
}
