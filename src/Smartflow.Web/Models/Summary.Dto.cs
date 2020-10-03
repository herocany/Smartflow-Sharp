using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class SummaryDto
    {
        public string CategoryName
        {
            get;
            set;
        }
        public string InstanceID
        {
            get;
            set;
        }
        public string CategoryID
        {
            get;
            set;

        }
        public string Comment
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public string Creator
        {
            get;
            set;

        }
        public DateTime CreateDateTime
        {
            get;
            set;
        }

        public string NodeName
        {
            get;
            set;
        }

        public string NodeID
        {
            get;set;
        }

        public string State
        {
            get;
            set;
        }

        public string StateName
        {
            get
            {
                string result = this.State.ToLower();
                if (result == "running")
                {
                    return "流程运行中";
                }
                else if (result == "start")
                {
                    return "流程开始";
                }
                else if (result == "kill" || result == "reject")
                {
                    return "流程终止";
                }
                else if (result == "end")
                {
                    return "流程结束";
                }
                return result;
            }
        }
        public string RealName
        {
            get;
            set;
        }

        public string OrganizationName
        {
            get;
            set;
        }
    }
}