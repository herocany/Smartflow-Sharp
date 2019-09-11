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


namespace Smartflow.Elements
{
    public class Transition : Element, IRelationship
    {
        private string destination = string.Empty;
        private string expression = string.Empty;
       
        public string RelationshipID
        {
            get;
            set;
        }

        public string Origin
        {
            get;
            set;
        }
     
     
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

     
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
    }
}
