using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using System.Xml.Linq;
using Action = Smartflow.Elements.Action;
using System.Data;
using Dapper;
using Smartflow.Components;

namespace Smartflow
{
    public class WorkflowNodeService : WorkflowInfrastructure, IWorkflowNodeService, IWorkflowPersistent<Element, Action<string, object>>, IWorkflowQuery<IList<Node>, string>, IWorkflowParse
    {
        public IWorkflowQuery<IList<WorkflowConfiguration>> ConfigurationService
        {
            get { return new WorkflowConfigurationService(); }
        }

        protected WorkflowTransitionService TransitionService
        {
            get
            {
                return new WorkflowTransitionService();
            }
        }

        protected WorkflowActionService ActionService
        {
            get
            {
                return new WorkflowActionService();
            }
        }

        protected WorkflowGroupService GroupService
        {

            get
            {
                return new WorkflowGroupService();
            }
        }

        protected WorkflowActorService ActorService
        {
            get
            {

                return new WorkflowActorService();
            }
        }

        protected WorkflowCommandService CommandService
        {
            get
            {
                return new WorkflowCommandService();
            }
        }

        protected WorkflowRuleService RuleService
        {
            get
            {
                return new WorkflowRuleService();
            }
        }

        protected IWorkflowProcessService ProcessService
        {
            get { return WorkflowGlobalServiceProvider.Resolve<IWorkflowProcessService>(); }
        }

        public IWorkflowCooperationService WorkflowCooperationService
        {
            get { return WorkflowGlobalServiceProvider.Resolve<IWorkflowCooperationService>(); }
        }

        public Element Parse(XElement element)
        {
            Node node = new Node
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value,
                Cooperation = Convert.ToInt32(element.Attribute("cooperation").Value)
            };

            string category = element.Attribute("category").Value;
            node.NodeType = Utils.Convert(category);

            if (element.HasElements)
            {
                List<Element> nodes = new List<Element>();
                element.Elements().ToList().ForEach(entry =>
                {
                    string nodeName = entry.Name.LocalName;
                    if (ServiceContainer.Contains(nodeName))
                    {
                        IWorkflowParse parseService = (ServiceContainer.Resolve(nodeName) as IWorkflowParse);
                        nodes.Add(parseService.Parse(entry));
                    }
                });

                node.Transitions.AddRange(nodes.Where(transition => (transition is Transition)).Cast<Transition>());
                node.Actions.AddRange(nodes.Where(action => (action is Action)).Cast<Action>());
                node.Groups.AddRange(nodes.Where(group => (group is Group)).Cast<Group>());
                node.Actors.AddRange(nodes.Where(actor => (actor is Actor)).Cast<Actor>());
                node.Rules.AddRange(nodes.Where(rule => (rule is Elements.Rule)).Cast<Elements.Rule>());
                node.Command = nodes.Where(command => (command is Command)).Cast<Command>().FirstOrDefault();
            }
            return node;
        }

        public void Persistent(Element entry, Action<string, object> execute)
        {
            Node n = (entry as Node);
            entry.NID = Guid.NewGuid().ToString();
            execute(ResourceManage.SQL_WORKFLOW_NODE_INSERT, new { entry.NID, entry.ID, entry.Name, NodeType = n.NodeType.ToString(), entry.InstanceID, n.Cooperation });

            foreach (Transition transition in n.Transitions)
            {
                transition.RelationshipID = entry.NID;
                transition.Origin = entry.ID;
                transition.InstanceID = entry.InstanceID;
                TransitionService.Persistent(transition, execute);
            }

            foreach (Elements.Action a in n.Actions)
            {
                a.RelationshipID = entry.NID;
                a.InstanceID = entry.InstanceID;
                ActionService.Persistent(a, execute);
            }

            foreach (Group r in n.Groups)
            {
                r.RelationshipID = entry.NID;
                r.InstanceID = entry.InstanceID;
                GroupService.Persistent(r, execute);
            }

            foreach (Actor a in n.Actors)
            {
                a.RelationshipID = entry.NID;
                a.InstanceID = entry.InstanceID;
                ActorService.Persistent(a, execute);
            }

            foreach (Elements.Rule r in n.Rules)
            {
                r.RelationshipID = entry.NID;
                r.InstanceID = entry.InstanceID;
                RuleService.Persistent(r, execute);
            }

            if (n.Command != null)
            {
                n.Command.InstanceID = entry.InstanceID;
                n.Command.RelationshipID = entry.NID;

                CommandService.Persistent(n.Command, execute);
            }
        }

        public IList<Node> Query(string instanceID)
        {
            IList<Node> nodes = new List<Node>();

            base.Connection.Query<Node>(ResourceManage.SQL_WORKFLOW_NODE_SELECT, new { InstanceID = instanceID }).ToList().ForEach(e =>
              {
                  nodes.Add(GetNode(e));
              });

            return nodes;
        }


        /// <summary>
        /// 动态获取路线，根据决策节点设置条件表达式，自动去判断流转的路线
        /// </summary>
        /// <returns>路线</returns>
        public Transition GetTransition(ASTNode n)
        {
            Command command = this.CommandService.Query(n.InstanceID)
                 .Where(e => e.RelationshipID == n.NID)
                 .FirstOrDefault();

            IList<WorkflowConfiguration> settings = ConfigurationService.Query();
            WorkflowConfiguration config = settings
                .Where(cfg => cfg.ID == long.Parse(command.ID))
                .FirstOrDefault();

            IDbConnection connection = DbFactory.CreateConnection(config.ProviderName, config.ConnectionString);
            try
            {
                DataTable resultSet = new DataTable(Guid.NewGuid().ToString());
                using (IDataReader reader = connection.ExecuteReader(command.Text, new { n.InstanceID }))
                {
                    resultSet.Load(reader);
                    reader.Close();
                }
                Transition instance = null;
                List<Transition> transitions =
                    this.TransitionService
                        .Query(n.InstanceID)
                        .Where(t => t.RelationshipID == n.NID)
                        .ToList();

                if (resultSet.Rows.Count > 0)
                {
                    foreach (Transition transition in transitions)
                    {
                        if (!String.IsNullOrEmpty(transition.Expression) && resultSet.Select(transition.Expression).Length > 0)
                        {
                            instance = transition;
                            break;
                        }
                    }
                }

                resultSet.Dispose();
                return instance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Node GetNode(Node entry)
        {
            entry.Transitions = TransitionService.Query(entry.InstanceID).Where(t => t.RelationshipID == entry.NID).ToList();
            entry.Groups = GroupService.Query(entry.InstanceID).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Actors = ActorService.Query(entry.InstanceID).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Actions = ActionService.Query(entry.InstanceID).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Command = CommandService.Query(entry.InstanceID).Where(e => e.RelationshipID == entry.NID).FirstOrDefault();
            entry.Rules = RuleService.Query(entry.InstanceID).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Previous = GetPrevious(entry);
            return entry;
        }

        public List<Transition> GetExecuteTransition(WorkflowInstance instance)
        {
            List<Transition> transitions = new List<Transition>();
            Node entry = instance.Current;
            Node previous = entry.Previous;
            transitions.AddRange(entry.Transitions.Where(t => t.Direction == WorkflowOpertaion.Go));
            if (previous != null && instance.Mode == WorkflowMode.Mix)
            {
                transitions.AddRange(entry.Transitions.Where(t => t.Destination == previous.ID && t.Direction == WorkflowOpertaion.Back));
                transitions.Add(Utils.CONST_REJECT_TRANSITION);
            }
            else if (WorkflowMode.Transition == instance.Mode&&entry.NodeType!=WorkflowNodeCategory.Start)
            {
                transitions.Add(Utils.CONST_REJECT_TRANSITION);
            }
            return transitions;
        }

        public Transition GetBackTransition(Node current)
        {
            var previous = current.Previous;

            return current.Transitions.FirstOrDefault(t =>
                    t.Destination == previous.ID && t.Direction == WorkflowOpertaion.Back);
        }

        private Node FindNodeByID(string ID, string instanceID)
        {
            Node entry = base.Connection.Query<Node>(ResourceManage.SQL_WORKFLOW_NODE_SELECT_ID, new { InstanceID = instanceID, ID }).FirstOrDefault();
            return entry ?? GetNode(entry);
        }

        /// <summary>
        /// 上一个执行跳转节点
        /// </summary>
        /// <returns></returns>
        public Node GetPrevious(Node entry)
        {
            Transition transition = GetHistoryTransition(entry);
            if (transition == null) return null;
            WorkflowMode mode = WorkflowGlobalServiceProvider.Resolve<IWorkflowInstanceService>().GetMode(entry.InstanceID);
            if (mode == WorkflowMode.Mix)
            {
                Node wrapNode = this.FindNodeByID(transition.Origin, entry.InstanceID);
                return this.GetNode(wrapNode);
            }
            else
            {
                return base.Connection.Query<Node>(ResourceManage.SQL_WORKFLOW_NODE_SELECT_ID, new {  entry.InstanceID,ID=transition.Origin }).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取回退线路
        /// </summary>
        /// <returns>路线</returns>
        protected Transition GetHistoryTransition(Node entry)
        {
            Transition transition = null;

            Dictionary<String, Object> queryArg = new Dictionary<string, object>
                {
                    { "InstanceID", entry.InstanceID },
                    { "Direction",(int)WorkflowOpertaion.Go}
                };

            WorkflowProcess process = ProcessService
                                        .Query(queryArg).Where(c => c.Destination == entry.ID)
                                        .FirstOrDefault();

            if (process != null && entry.NodeType != WorkflowNodeCategory.Start)
            {
                transition = TransitionService.Query(entry.InstanceID).FirstOrDefault(e => e.NID == process.TransitionID && e.Direction == WorkflowOpertaion.Go);
            }

            return transition;
        }
    }
}
