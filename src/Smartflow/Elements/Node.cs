/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Smartflow;
using System.Xml.Linq;

namespace Smartflow.Elements
{
    public class Node : ASTNode
    {
        protected List<Actor> actors = new List<Actor>();
        protected List<Group> groups = new List<Group>();

        protected Command command;

        public Command Command
        {
            get { return command; }
            set { command = value; }
        }

        public List<Group> Groups
        {
            get { return groups; }
            set { groups = value; }
        }

        public List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; }
        }
    }
}
