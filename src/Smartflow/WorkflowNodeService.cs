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
    public class WorkflowNodeService : WorkflowInfrastructure, IWorkflowPersistent<Node>, IWorkflowQuery<Node>, IWorkflowParse
    {
        public WorkflowTransitionService TransitionService
        {
            get
            {
                return new WorkflowTransitionService();
            }
        }
        public WorkflowActionService ActionService
        {
            get
            {
                return new WorkflowActionService();
            }
        }

        public WorkflowGroupService GroupService
        {

            get
            {
                return new WorkflowGroupService();
            }
        }
        public WorkflowActorService ActorService
        {
            get
            {

                return new WorkflowActorService();
            }
        }

        public WorkflowCommandService CommandService
        {
            get
            {
                return new WorkflowCommandService();
            }
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

        public void Persistent(Node entry)
        {
            entry.NID = Guid.NewGuid().ToString();
            string sql = "INSERT INTO T_NODE(NID,ID,Name,NodeType,InstanceID,Cooperation,Increment) VALUES(@NID,@ID,@Name,@NodeType,@InstanceID,@Cooperation,@Increment)";
            base.Connection.Execute(sql, new { entry.NID, entry.ID, entry.Name, NodeType=entry.NodeType.ToString(), entry.InstanceID, entry.Cooperation, entry.Increment });

            foreach (Transition transition in entry.Transitions)
            {
                transition.RelationshipID = entry.NID;
                transition.Origin = entry.ID;
                transition.InstanceID = entry.InstanceID;
                TransitionService.Persistent(transition);
            }

            foreach (Elements.Action a in entry.Actions)
            {
                a.RelationshipID = entry.NID;
                a.InstanceID = entry.InstanceID;
                ActionService.Persistent(a);
            }

            foreach (Group r in entry.Groups)
            {
                r.RelationshipID = entry.NID;
                r.InstanceID = entry.InstanceID;
                GroupService.Persistent(r);
            }

            foreach (Actor a in entry.Actors)
            {
                a.RelationshipID = entry.NID;
                a.InstanceID = entry.InstanceID;
                ActorService.Persistent(a);
            }

            if (entry.Command != null)
            {
                entry.Command.InstanceID = entry.InstanceID;
                entry.Command.RelationshipID = entry.NID;

                CommandService.Persistent(entry.Command);
            }
        }

        public IList<Node> Query(object condition)
        {
            string query = "SELECT * FROM T_NODE WHERE  InstanceID=@InstanceID";
            return Connection.Query<Node>(query, condition).ToList();
        }

        internal void DoIncrement(Node node)
        {
            node.Increment += 1;
            string sql = "UPDATE T_NODE SET Increment=@Increment WHERE InstanceID=@InstanceID AND  NID=@NID ";
            Connection.ExecuteScalar<long>(sql, new { node.NID, node.InstanceID, node.Increment });
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

            IList<WorkflowConfiguration> settings = new WorkflowConfigurationService().Query(Utils.Empty);
            WorkflowConfiguration config = settings
                .Where(cfg => cfg.ID == long.Parse(command.ID))
                .FirstOrDefault();

            IDbConnection connection = DapperFactory.CreateConnection(config.ProviderName, config.ConnectionString);
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
    }
}
