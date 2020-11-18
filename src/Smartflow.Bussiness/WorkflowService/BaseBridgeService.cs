/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper;
using Smartflow;
using System.Configuration;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Elements;
using Smartflow.Bussiness.Interfaces;

namespace Smartflow.Bussiness.WorkflowService
{
    public class BaseBridgeService : AbstractBridgeService
    {
        private readonly IDbConnection connection = DBUtils.CreateConnection();
        private readonly IActorService _actorService = new ActorService();

        public override List<WorkflowGroup> GetGroup()
        {
            string query = " SELECT * FROM T_SYS_ROLE WHERE 1=1 AND Type=0 ";
            List<WorkflowGroup> groupList = new List<WorkflowGroup>();
            using (IDataReader dr = connection.ExecuteReader(query))
            {
                while (dr.Read())
                {
                    groupList.Add(new WorkflowGroup()
                    {
                        ID = dr["ID"].ToString(),
                        Name = dr["Name"].ToString()
                    });
                }
            }
            return groupList;
        }

        public override List<WorkflowActor> GetActor(int pageIndex, int pageSize, out int total, Dictionary<string, string> queryArg)
        {
            string conditionStr = string.Empty;
            if (queryArg.ContainsKey("actor"))
            {
                conditionStr = string.Format(" AND ID NOT IN ({0})", Utils.BindQuot(queryArg["actor"]));
            }
            if (queryArg.ContainsKey("searchKey"))
            {
                conditionStr = string.Format("{0} AND Name LIKE '%{1}%'", conditionStr, queryArg["searchKey"]);
            }

            if (queryArg.ContainsKey("orgCode"))
            {
                conditionStr = string.Format("{0} AND OrganizationCode LIKE '{1}%'", conditionStr, queryArg["orgCode"]);
            }

            string query = String.Format("SELECT TOP {0} *,(SELECT Name FROM[dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName  FROM T_SYS_USER WHERE Type=0 AND Status=0 AND ID NOT IN (SELECT TOP {1} ID  FROM T_SYS_USER WHERE 1=1 {2} ORDER BY Type ASC) {2}  ORDER BY Type ASC ", pageSize, pageSize * (pageIndex - 1), conditionStr);
            total = connection.ExecuteScalar<int>(String.Format("SELECT COUNT(1) FROM T_SYS_USER WHERE 1=1 AND Type=0 AND Status=0   {0}", conditionStr));
            List<WorkflowActor> actors = new List<WorkflowActor>();
            using (var dr = connection.ExecuteReader(query))
            {
                while (dr.Read())
                {
                    actors.Add(new WorkflowActor()
                    {
                        ID = dr["ID"].ToString(),
                        Name = string.Format("{0}", dr["Name"]),
                        Data = new
                        {
                            OrgName = dr["OrganizationName"],
                            OrgCode = dr["OrganizationCode"]
                        }
                    });
                }
            }
            return actors;
        }

        public override List<WorkflowActor> GetActor(Dictionary<string, string> queryArg)
        {
            if (!queryArg.ContainsKey("actor"))
            {
                return new List<WorkflowActor>();
            }
            else
            {
                string conditionStr = string.Format(" AND ID IN ({0})", Utils.BindQuot(queryArg["actor"]));
                string query = String.Format(" SELECT *,(SELECT Name FROM[dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName  FROM T_SYS_USER WHERE 1=1 AND Type=0 AND Status=0 {0} ORDER BY Type  ", conditionStr);
                List<WorkflowActor> actors = new List<WorkflowActor>();
                using (var dr = connection.ExecuteReader(query))
                {
                    while (dr.Read())
                    {
                        actors.Add(new WorkflowActor()
                        {
                            ID = dr["ID"].ToString(),
                            Name = string.Format("{0}", dr["Name"]),
                            Data = new
                            {
                                OrgName = dr["OrganizationName"],
                                OrgCode = dr["OrganizationCode"]
                            }
                        });
                    }
                }
                return actors;
            }
        }

        public List<User> GetActorByGroup(Node node, WorkflowOpertaion direction)
        {
            dynamic dyObject = ConvertStr(node);
            return GetActorByGroup((String)dyObject.Actors, (String)dyObject.Groups, (String)dyObject.Organizations, node, direction);
        }

        public List<User> GetActorByGroup(string actors, string groups, string organizations, Node node, WorkflowOpertaion direction)
        {
            if (direction == WorkflowOpertaion.Back && String.IsNullOrEmpty(node.Cooperation))
            {
                dynamic dyObject = ConvertStr(node);
                List<User> userList = GetAllActor((String)dyObject.Actors, (String)dyObject.Groups, (String)dyObject.Organizations);
                WorkflowProcess process = base.ProcessService
                                         .Get(node.InstanceID)
                                         .Where(n => n.Origin == node.ID)
                                         .OrderByDescending(e => e.CreateTime)
                                         .FirstOrDefault();

                return userList.Where(e => e.ID == process.ActorID).ToList();
            }
            else
            {
                List<User> userList = GetAllActor(actors, groups, organizations);
                if (node.Rules.Count > 0)
                {
                    userList = QueryActor(userList, node.Rules);
                }
                return userList
                    .ToLookup(p => p.ID)
                    .Select(c => c.First())
                    .ToList();
            }
        }

        private List<User> GetAllActor(string actors, string groups, string organizations)
        {
            List<User> userList = new List<User>();

            if (!String.IsNullOrEmpty(groups))
            {
                userList.AddRange(_actorService.GetActorByRole(Utils.BindQuot(groups)));
            }
            if (!String.IsNullOrEmpty(organizations))
            {
                userList.AddRange(_actorService.GetActorByOrganization(Utils.BindQuot(organizations)));
            }
            if (!String.IsNullOrEmpty(actors))
            {
                userList.AddRange(_actorService.Query(Utils.BindQuot(actors)));
            }
            return userList;
        }

        private dynamic ConvertStr(Node node)
        {
            List<string> gList = new List<string>();
            List<string> ids = new List<string>();
            List<string> orgIds = new List<string>();
            foreach (Group g in node.Groups)
            {
                gList.Add(g.ID.ToString());
            }
            foreach (Actor item in node.Actors)
            {
                ids.Add(item.ID);
            }
            foreach (Smartflow.Elements.Organization item in node.Organizations)
            {
                orgIds.Add(item.ID);
            }
            return new
            {
                Actors = string.Join(",", ids),
                Groups = string.Join(",", gList),
                Organizations = string.Join(",", orgIds)
            };
        }

        public List<User> GetCarbonCopies(Node node, string carbons)
        {
            List<User> userList = new List<User>();
            if (!String.IsNullOrEmpty(carbons))
            {
                userList.AddRange(_actorService.Query(Utils.BindQuot(carbons)));
            }
            else
            {
                List<string> userStrs = new List<string>();
                foreach (Carbon c in node.Carbons)
                {
                    userStrs.Add(c.ID.ToString());
                }
                if (userStrs.Count > 0)
                {
                    userList.AddRange(_actorService.Query(Utils.BindQuot(String.Join(",", userStrs))));
                }
            }

            return userList
                   .ToLookup(p => p.ID)
                   .Select(c => c.First())
                   .ToList();
        }

        private List<User> QueryActor(IList<User> users, IList<Smartflow.Elements.Rule> rules)
        {
            List<User> actorList = new List<User>();
            foreach (Smartflow.Elements.Rule rule in rules)
            {
                WorkflowRuleType ruleType = (WorkflowRuleType)Enum.Parse(typeof(WorkflowRuleType), rule.ID, true);
                if (ruleType == WorkflowRuleType.NODE_SEND_START_USER)
                {
                    WorkflowProcess process = base.ProcessService.Get(rule.InstanceID)
                                                .OrderByDescending(e => e.CreateTime)
                                                .FirstOrDefault(e => e.NodeType == WorkflowNodeCategory.Start);

                    if (process != null)
                    {
                        String orgCode = _actorService.GetOrganizationCode(process.ActorID);
                        actorList.AddRange(users.Where(u => u.OrganizationCode == orgCode).ToList());
                    }
                }
                else
                {
                    WorkflowProcess process = base.ProcessService.Get(rule.InstanceID).OrderByDescending(e => e.CreateTime).FirstOrDefault();
                    if (process != null)
                    {
                        String orgCode = _actorService.GetOrganizationCode(process.ActorID);
                        actorList.AddRange(users.Where(u => u.OrganizationCode == orgCode).ToList());
                    }
                }
            }
            return actorList;
        }

        public override List<WorkflowCarbon> GetCarbon(int pageIndex, int pageSize, out int total, Dictionary<string, string> queryArg)
        {
            string conditionStr = string.Empty;
            if (queryArg.ContainsKey("carbon"))
            {
                conditionStr = string.Format(" AND ID NOT IN ({0})", Utils.BindQuot(queryArg["carbon"]));
            }
            if (queryArg.ContainsKey("searchKey"))
            {
                conditionStr = string.Format("{0} AND Name LIKE '%{1}%'", conditionStr, queryArg["searchKey"]);
            }

            if (queryArg.ContainsKey("orgCode"))
            {
                conditionStr = string.Format("{0} AND OrganizationCode LIKE '{1}%'", conditionStr, queryArg["orgCode"]);
            }

            string query = String.Format("SELECT TOP {0} *,(SELECT Name FROM[dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName FROM T_SYS_USER WHERE ID NOT IN (SELECT TOP {1} ID  FROM T_SYS_USER WHERE 1=1 {2} ORDER BY Type ASC) {2}  ORDER BY Type ASC ", pageSize, pageSize * (pageIndex - 1), conditionStr);
            total = connection.ExecuteScalar<int>(String.Format("SELECT COUNT(1) FROM T_SYS_USER WHERE 1=1 {0}", conditionStr));
            List<WorkflowCarbon> carbons = new List<WorkflowCarbon>();
            using (var dr = connection.ExecuteReader(query))
            {
                while (dr.Read())
                {
                    carbons.Add(new WorkflowCarbon()
                    {
                        ID = dr["ID"].ToString(),
                        Name = string.Format("{0}", dr["Name"]),
                        Data = new
                        {
                            OrgName = dr["OrganizationName"],
                            OrgCode = dr["OrganizationCode"]
                        }
                    });
                }
            }
            return carbons;
        }

        public override List<WorkflowCarbon> GetCarbon(Dictionary<string, string> queryArg)
        {
            if (!queryArg.ContainsKey("carbon"))
            {
                return new List<WorkflowCarbon>();
            }
            else
            {
                string conditionStr = string.Format(" AND ID IN ({0})", Utils.BindQuot(queryArg["carbon"]));
                string query = String.Format(" SELECT *,(SELECT Name FROM[dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName  FROM T_SYS_USER WHERE 1=1 {0} ORDER BY Type ASC ", conditionStr);
                List<WorkflowCarbon> carbons = new List<WorkflowCarbon>();
                using (var dr = connection.ExecuteReader(query))
                {
                    while (dr.Read())
                    {
                        carbons.Add(new WorkflowCarbon()
                        {
                            ID = dr["ID"].ToString(),
                            Name = string.Format("{0}", dr["Name"]),
                            Data = new
                            {
                                OrgName = dr["OrganizationName"],
                                OrgCode = dr["OrganizationCode"]
                            }
                        });
                    }
                }
                return carbons;
            }
        }
    }
}

