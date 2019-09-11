using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Elements
{
    public class Action : Element, IRelationship
    {
        public string RelationshipID
        {
            get;
            set;
        }
    }
}
