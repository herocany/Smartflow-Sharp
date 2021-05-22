using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Models
{
    public class Supervise
    {
        public virtual string CategoryName
        {
            get;
            set;
        }
        public virtual string InstanceID
        {
            get;
            set;
        }
        public virtual string CategoryCode
        {
            get;
            set;

        }
        public virtual string Comment
        {
            get;
            set;
        }
        public virtual string Key
        {
            get;
            set;
        }
        public virtual string Creator
        {
            get;
            set;

        }
        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        public virtual string NodeName
        {
            get;
            set;
        }

        public virtual string State
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }


        public virtual string NodeID
        {
            get;
            set;
        }


        public virtual string OrganizationName
        {
            get;
            set;
        }
    }
}
