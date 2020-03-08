using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.Models;

namespace Smartflow.Bussiness.WorkflowService
{
    public class FormService
    {
        public static void Execute(string cateCode, string instanceID)
        {
            Category model = new CategoryQueryService().Query()
              .FirstOrDefault(cate => cate.NID == cateCode);

            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            var current = instance.Current;

            if (!String.IsNullOrEmpty(model.Script) && current.NodeType != WorkflowNodeCategory.Start)
            {
                var cate = new BridgeQueryService().Query(instanceID);
                if (cate == null) return;
                DBUtils.CreateConnection().Execute(model.Script, new
                {
                    NodeName = instance.State == WorkflowInstanceState.Running ? current.Name : string.Format("{0}({1})", current.Name, "否决"),
                    NID = cate.Key
                });
            }
        }
    }
}
