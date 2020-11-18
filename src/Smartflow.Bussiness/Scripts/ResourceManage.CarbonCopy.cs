using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region CreateCarbonCopy\CarbonCopyQueryService
        public const string SQL_CARBONCOPY_INSERT = @"INSERT INTO T_CARBONCOPY(NID,ActorID,InstanceID,NodeID,CreateTime) VALUES(@NID,@ActorID,@InstanceID,@NodeID,@CreateTime)";
        #endregion
    }
}
