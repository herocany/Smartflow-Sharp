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

namespace Smartflow.Elements
{
    public class Command : Element, IRelationship
    {
        private string text = string.Empty;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    
        public string RelationshipID
        {
            get;
            set;
        }
    }
}
