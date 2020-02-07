using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region CreateBridge\BridgeQueryService
        public const string SQL_BRIDGE_INSERT = @"INSERT INTO T_Bridge(InstanceID,CategoryID,FormID) VALUES(@InstanceID,@CategoryID,@FormID)";
        public const string SQL_BRIDGE_SELECT_BY_INSTANCEID = "SELECT * FROM T_Bridge Where InstanceID=@InstanceID   ";
        public const string SQL_BRIDGE_SELECT= "SELECT * FROM T_Bridge Where FormID=@FormID AND @CategoryID=@CategoryID   ";
        #endregion
    }
}
