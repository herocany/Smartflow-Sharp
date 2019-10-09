using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using System.Xml.Linq;
using Action = Smartflow.Elements.Action;
using System.Data;

namespace Smartflow
{
    public class WorkflowNodeService : WorkflowInfrastructure, IWorkflowNodeService, IWorkflowPersistent<Element>, IWorkflowQuery<Node>, IWorkflowParse
    {
        public IWorkflowQuery<WorkflowConfiguration> ConfigurationService
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

        protected IWorkflowProcessService ProcessService
        {
            get { return WorkflowGlobalServiceProvider.Resolve<IWorkflowProcessService>(); }
        }


        public Element Parse(XElement element)
        {
            Node node = new Node();

            node.Name = element.Attribute("name").Value;
            node.ID = element.Attribute("id").Value;
            node.Cooperation = Convert.ToInt32(element.Attribute("cooperation").Value);
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
                node.Command = nodes.Where(command => (command is Command)).Cast<Command>().FirstOrDefault();
            }
            return node;
        }

        public void Persistent(Element entry)
        {
            Node n = (entry as Node);

            entry.NID = Guid.NewGuid().ToString();
            string sql = "INSERT INTO T_NODE(NID,ID,Name,NodeType,InstanceID,Cooperation) VALUES(@NID,@ID,@Name,@NodeType,@InstanceID,@Cooperation)";
            base.Connection.Execute(sql, new { entry.NID, entry.ID, entry.Name, NodeType = n.NodeType.ToString(), entry.InstanceID, n.Cooperation });

            foreach (Transition transition in n.Transitions)
            {
                transition.RelationshipID = entry.NID;
                transition.Origin = entry.ID;
                transition.InstanceID = entry.InstanceID;
                TransitionService.Persistent(transition);
            }

            foreach (Elements.Action a in n.Actions)
            {
                a.RelationshipID = entry.NID;
                a.InstanceID = entry.InstanceID;
                ActionService.Persistent(a);
            }

            foreach (Group r in n.Groups)
            {
                r.RelationshipID = entry.NID;
                r.InstanceID = entry.InstanceID;
                GroupService.Persistent(r);
            }

            foreach (Actor a in n.Actors)
            {
                a.RelationshipID = entry.NID;
                a.InstanceID = entry.InstanceID;
                ActorService.Persistent(a);
            }

            if (n.Command != null)
            {
                n.Command.InstanceID = entry.InstanceID;
                n.Command.RelationshipID = entry.NID;

                CommandService.Persistent(n.Command);
            }
        }

        public IList<Node> Query(object condition)
        {
            IList<Node> nodes = new List<Node>();

            string query = "SELECT * FROM T_NODE WHERE  InstanceID=@InstanceID";
            base.Connection.Query<Node>(query, condition).ToList().ForEach(e =>
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
            Command command = this.CommandService.Query(new { n.InstanceID })
                 .Where(e => e.RelationshipID == n.NID)
                 .FirstOrDefault();

            IList<WorkflowConfiguration> settings = ConfigurationService.Query(Utils.Empty);
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
                        .Query(new { n.InstanceID })
                        .Where(t => t.RelationshipID == n.NID)
                        .ToList();

                if (resultSet.Rows.Count > 0)
                {
                    foreach (Transition transition in transitions)
                    {
                        if (resultSet.Select(transition.Expression).Length > 0)
                        {
                            instance = transition;
                            break;
                        }
                    }
                }
                return instance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Node GetNode(Node entry)
        {
            entry.Transitions = TransitionService.Query(new { entry.InstanceID }).Where(t => t.RelationshipID == entry.NID).ToList();
            entry.Groups = GroupService.Query(new { entry.InstanceID }).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Actors = ActorService.Query(new { entry.InstanceID }).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Actions = ActionService.Query(new { entry.InstanceID }).Where(e => e.RelationshipID == entry.NID).ToList();
            entry.Command = CommandService.Query(new { entry.InstanceID }).Where(e => e.RelationshipID == entry.NID).FirstOrDefault();
            entry.Previous = GetPrevious(entry);
            return entry;
        }

        public List<Transition> GetExecuteTransitions(WorkflowInstance instance)
        {
            List<Transition> transitions = new List<Transition>();
            Node entry = instance.Current;
            Node previous = entry.Previous;

            foreach (Smartflow.Elements.Transition transition in entry.Transitions)
            {
                ASTNode an = this.FindNodeByID(transition.Destination, entry.InstanceID);

                Transition decisionTransition = transition;
                while (an.NodeType == WorkflowNodeCategory.Decision)
                {
                    decisionTransition = this.GetTransition(an);
                    an = this.FindNodeByID(decisionTransition.Destination, entry.InstanceID);
                }
                transition.Name = decisionTransition.Name;
            }

            transitions.AddRange(entry.Transitions);

            bool support = (previous != null && previous.Cooperation == 0 && entry.Cooperation == 0 && instance.Mode == WorkflowMode.Mix);

            if (support)
            {
                transitions.Add(new Transition
                {
                    NID = "back",
                    Name = "原路退回"

                });
            }

            return transitions;
        }

        private Node FindNodeByID(string ID, string instanceID)
        {
            string query = "SELECT * FROM T_NODE WHERE  InstanceID=@InstanceID AND ID=@ID";
            Node entry = base.Connection.Query<Node>(query, new { InstanceID = instanceID, ID }).FirstOrDefault();
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

            Node wrapNode = this.FindNodeByID(transition.Origin, entry.InstanceID);

            return this.GetNode(wrapNode);
        }

        /// <summary>
        /// 获取回退线路
        /// </summary>
        /// <returns>路线</returns>
        protected Transition GetHistoryTransition(Node entry)
        {
            Transition transition = null;

            WorkflowProcess process = ProcessService.Query(new { entry.InstanceID, Command = 0 })
                .Where(c => c.Destination == entry.ID)
                .FirstOrDefault();

            if (process != null && entry.NodeType != WorkflowNodeCategory.Start)
            {
                ASTNode n = this.FindNodeByID(process.Origin, entry.InstanceID);

                while (n.NodeType == WorkflowNodeCategory.Decision)
                {
                    process = ProcessService.Query(new { entry.InstanceID, Command = 0 })
                             .FirstOrDefault(c => c.Destination == n.ID);

                    n = this.FindNodeByID(process.Origin, entry.InstanceID);

                    if (n.NodeType == WorkflowNodeCategory.Start)
                        break;
                }

                transition =
                     TransitionService.Query(new { entry.InstanceID }).FirstOrDefault(e => e.NID == process.TransitionID);
            }
            return transition;
        }
    }
}
