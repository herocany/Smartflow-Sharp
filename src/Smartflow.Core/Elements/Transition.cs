/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Smartflow;

namespace Smartflow.Core.Elements
{
    public class Transition : Element
    {
        private string destination = string.Empty;
        private string expression = string.Empty;

        public virtual string RelationshipID
        {
            get;
            set;
        }

        public virtual string Origin
        {
            get;
            set;
        }

        public virtual string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
     
        public virtual string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
    }
}
