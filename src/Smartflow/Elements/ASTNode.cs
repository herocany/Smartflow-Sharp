/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Elements
{
    public abstract  class ASTNode : Element
    {
        protected List<Transition> transitions = new List<Transition>();
        protected WorkflowNodeCategory category = WorkflowNodeCategory.Node;
        protected List<Action> actions = new List<Action>();

        private int cooperation = 0;

        private int increment = 0;

        public List<Action> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        public List<Transition> Transitions
        {
            get { return transitions; }
            set { transitions = value; }
        }

        public WorkflowNodeCategory NodeType
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// 是否会签 （协办），默认是不参与协办
        /// </summary>
        public virtual int Cooperation
        {
            get { return cooperation; }
            set { cooperation = value; }
        }

        public int Increment
        {
            get { return increment; }
            set { increment = value; }
        }

        //internal virtual List<Transition> QueryWorkflowNode(string relationshipID)
        //{
        //    string query = "SELECT * FROM T_TRANSITION WHERE RelationshipID=@RelationshipID";
        //    return Connection.Query<Transition>(query, new { RelationshipID = relationshipID }).ToList();
        //}

        //public ASTNode GetNode(string ID)
        //{
        //    string query = "SELECT * FROM T_NODE WHERE ID=@ID AND InstanceID=@InstanceID";
        //    return Connection.Query<Node>(query, new
        //    {
        //        ID = ID,
        //        InstanceID = InstanceID
        //    }).FirstOrDefault();
        //}
    }
}