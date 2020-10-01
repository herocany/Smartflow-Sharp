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
    public abstract class ASTNode : Element
    {
        protected List<Transition> transitions = new List<Transition>();
        protected WorkflowNodeCategory category = WorkflowNodeCategory.Node;
        protected List<Action> actions = new List<Action>();
        private string cooperation = string.Empty;
        private string assistant = string.Empty;

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
        /// 流转策略 
        /// </summary>
        public virtual string Cooperation
        {
            get { return cooperation; }
            set { cooperation = value; }
        }

        /// <summary>
        /// 协办策略
        /// </summary>
        public virtual string Assistant
        {
            get { return assistant; }
            set { assistant = value; }
        }

    }
}