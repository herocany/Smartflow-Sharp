/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
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
