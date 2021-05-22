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
    public class Command : Element
    {
        private string text = string.Empty;

        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }
    
        public virtual string RelationshipID
        {
            get;
            set;
        }

        public virtual Node Node
        {
            get;
            set;
        }
    }
}
