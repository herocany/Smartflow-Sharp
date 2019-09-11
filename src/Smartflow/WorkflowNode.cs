/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using Smartflow.Elements;

namespace Smartflow
{
    public class WorkflowNode : Node
    {
        protected WorkflowNodeService NodeService
        {
            get { return new WorkflowNodeService(); }
        }

        protected WorkflowNode()
        {

        }

        public List<Transition> GetTransitions()
        {
            foreach (Smartflow.Elements.Transition transition in this.Transitions)
            {
                ASTNode an = FindNodeByID(transition.Destination);

                Transition decisionTransition = transition;
                while (an.NodeType == WorkflowNodeCategory.Decision)
                {
                    decisionTransition = NodeService.GetTransition(an);
                    an = FindNodeByID(decisionTransition.Destination);
                }
                transition.Name = decisionTransition.Name;
            }
            return this.Transitions;
        }

        private ASTNode FindNodeByID(string ID)
        {
           return NodeService.Query(new { InstanceID })
                   .Where(e => e.ID == ID).FirstOrDefault();
        }

        #region 节点方法

        public static WorkflowNode ConvertToReallyType(ASTNode node)
        {
            WorkflowNode wfNode = new WorkflowNode();
            wfNode.NID = node.NID;
            wfNode.ID = node.ID;
            wfNode.Name = node.Name;
            wfNode.NodeType = node.NodeType;
            wfNode.InstanceID = node.InstanceID;
            wfNode.Increment = node.Increment;
            wfNode.Cooperation = node.Cooperation;

            wfNode.Transitions = wfNode.NodeService
                .TransitionService
                .Query(new { node.InstanceID })
                .Where(t=>t.RelationshipID== node.NID).ToList();

            wfNode.Groups = wfNode.GetGroup();
            wfNode.Actors = wfNode.GetActors();
            wfNode.Actions = wfNode.GetActions();
            return wfNode;
        }

        protected List<Actor> GetActors()
        {
            return NodeService
                 .ActorService
                 .Query(new { InstanceID })
                 .Where(e => e.RelationshipID == NID).ToList();
        }

        protected List<Elements.Action> GetActions()
        {
            return NodeService
                  .ActionService
                  .Query(new { InstanceID })
                  .Where(e => e.RelationshipID == NID).ToList();
        }

        /// <summary>
        /// 依据主键获取路线
        /// </summary>
        /// <param name="TRANSITIONID">路线主键</param>
        /// <returns>路线</returns>
        protected Transition GetTransition(string transitionID)
        {
            return NodeService
                 .TransitionService
                 .Query(new { InstanceID })
                 .Where(e => e.NID == transitionID).FirstOrDefault();

        }

        protected List<Group> GetGroup()
        {
            return NodeService
                 .GroupService
                 .Query(new { InstanceID })
                 .Where(e => e.RelationshipID == NID).ToList();
        }

        #endregion
    }
}
