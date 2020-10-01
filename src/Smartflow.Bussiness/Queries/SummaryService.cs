using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class SummaryService : ISummaryService
    {
        public DataTable GetStat(string actor, string categoryID)
        {
            string query = @" SELECT
                            (SELECT Count(1) FROM T_PENDING p WHERE  p.ACTORID = @Actor AND p.InstanceID in (select InstanceID  FROM [dbo].[v_summary] WHERE  CategoryID Like @CategoryID)) Pending,
                            (SELECT Count(1) FROM [dbo].[v_summary]  WHERE CategoryID Like @CategoryID AND InstanceID  in (SELECT InstanceID  FROM [dbo].t_carbonCopy c  WHERE  c.ACTORID = @Actor )) Carbon,
                            (SELECT Count(1) FROM [dbo].[v_summary]  WHERE CategoryID Like @CategoryID  AND InstanceID in  (SELECT InstanceID FROM [dbo].t_record  WHERE  AuditUserID=@Actor) ) Record,
                            (SELECT Count(1) FROM v_summary s WHERE CategoryID Like @CategoryID  AND  s.Creator = @Actor ) Apply  ";

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Actor", actor);
            dynamicParameters.Add("CategoryID", string.Concat(categoryID, "%"));
            DataTable dataTable = new DataTable();
            using (IDataReader dataReader =DBUtils.CreateWFConnection().ExecuteReader(query, dynamicParameters))
            {
                dataTable.Load(dataReader);
            }
            return dataTable;
        }

        public IList<Summary> Query(Dictionary<string, string> queryArg)
        {
            string conditionStr = SetQueryArg(queryArg);
            string query = String.Format("SELECT * FROM V_SUMMARY WHERE 1=1 {0} ORDER BY CreateDateTime Desc ", conditionStr);
            return DBUtils.CreateWFConnection().Query<Summary>(query).ToList();
        }

        public IList<Summary> Query(Paging info, out int total)
        {
            string conditionStr = SetQueryArg(info.Get());
            string query = String.Format("SELECT TOP {0} * FROM V_SUMMARY WHERE InstanceID NOT IN (SELECT TOP {1} InstanceID FROM V_SUMMARY WHERE 1=1 {2} ORDER BY CreateDateTime Desc ) {2}  ORDER BY CreateDateTime Desc ", info.Limit, info.Limit * (info.Page - 1), conditionStr);
            total = DBUtils.CreateWFConnection().ExecuteScalar<int>(String.Format("SELECT COUNT(1) FROM V_SUMMARY WHERE 1=1 {0}", conditionStr));
            return DBUtils.CreateWFConnection().Query<Summary>(query).ToList();
        }

        public IList<Summary> QuerySupervise(Paging info, out int total)
        {
            string conditionStr = SetQueryArg(info.Get());
            string query = String.Format("SELECT TOP {0} * FROM V_SUPERVISE WHERE InstanceID NOT IN (SELECT TOP {1} InstanceID FROM V_SUPERVISE WHERE 1=1 {2} ORDER BY CreateDateTime Desc ) {2}  ORDER BY CreateDateTime Desc ", info.Limit, info.Limit * (info.Page - 1), conditionStr);
            total = DBUtils.CreateWFConnection().ExecuteScalar<int>(String.Format("SELECT COUNT(1) FROM V_SUPERVISE WHERE 1=1 {0}", conditionStr));
            return DBUtils.CreateWFConnection().Query<Summary>(query).ToList();
        }


        private string SetQueryArg(Dictionary<string, string> queryArg)
        {
            StringBuilder buildWhere = new StringBuilder();
            //待办事项
            if (queryArg.ContainsKey("type") && "0" == queryArg["type"])
            {
                buildWhere.AppendFormat("  AND InstanceID IN(SELECT InstanceID FROM T_PENDING WHERE  ACTORID = '{0}')", queryArg["actor"]);
            }

            //抄送
            else if (queryArg.ContainsKey("type") && "1" == queryArg["type"])
            {
                buildWhere.AppendFormat("  AND InstanceID IN(SELECT InstanceID FROM t_carbonCopy WHERE  ACTORID = '{0}')", queryArg["actor"]);
            }

            //审批过
            else if (queryArg.ContainsKey("type") && "2" == queryArg["type"])
            {
                buildWhere.AppendFormat("  AND InstanceID IN(SELECT InstanceID FROM t_record WHERE AuditUserID = '{0}')", queryArg["actor"]);
            }

            //申请的
            else if (queryArg.ContainsKey("type") && "3" == queryArg["type"])
            {
                buildWhere.AppendFormat("  AND  Creator = '{0}' ", queryArg["actor"]);
            }

            //实例表 查询监督信息
            else if (queryArg.ContainsKey("type") && "4" == queryArg["type"])
            {
                buildWhere.Append("  AND b.InstanceID IN(SELECT InstanceID FROM T_INSTANCE WHERE  State='Running')");
            }

            //申请的
            else if (queryArg.ContainsKey("type") && "5" == queryArg["type"])
            {
                buildWhere.AppendFormat("  AND  Creator = '{0}' AND InstanceID IN (SELECT InstanceID FROM T_INSTANCE WHERE  State='Running') ", queryArg["actor"]);
            }

            if (queryArg.ContainsKey("categoryID"))
            {
                buildWhere.AppendFormat("  AND CategoryID Like '{0}%'", queryArg["categoryID"]);
            }

            return buildWhere.ToString();
        }
    }
}
