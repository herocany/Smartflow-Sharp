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
        protected List<Rule> rules = new List<Rule>();
        protected List<Organization> organizations = new List<Organization>();
        protected List<Carbon> carbons = new List<Carbon>();
        

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

        public List<Organization> Organizations
        {
            get { return organizations; }
            set { organizations = value; }
        }

        public List<Carbon> Carbons
        {
            get { return carbons; }
            set { carbons = value; }
        }

        public List<Rule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        public Node Previous
        {
            get;
            set;
        }

        public string Veto
        {
            get;
            set;
        }

        public string Back
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }
    }
}
