using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region CreateBridge\BridgeQueryService     
        public const string SQL_BRIDGE_INSERT = @"INSERT INTO T_Bridge(InstanceID,CategoryCode,[Key],Comment,Creator,CreateTime) VALUES(@InstanceID,@CategoryCode,@Key,@Comment,@Creator,@CreateTime)";
        public const string SQL_BRIDGE_UPDATE = @"UPDATE T_Bridge SET InstanceID=@InstanceID WHERE [Key]=@Key";

        public const string SQL_BRIDGE_SELECT_BY_INSTANCEID = "SELECT [InstanceID],[CategoryCode],[Comment],[Key],[Creator],(SELECT Name FROM [Demo].dbo.T_SYS_USER where ID = Creator) Name,[CreateTime] FROM T_Bridge Where InstanceID=@InstanceID   ";
        public const string SQL_BRIDGE_SELECT= "SELECT * FROM T_Bridge Where [Key]=@Key AND CategoryCode=@CategoryCode   ";

        public const string SQL_BRIDGE_SELECT_BY_KEY = "SELECT [InstanceID],[CategoryCode],[Comment],[Key],[Creator],(SELECT Name FROM [Demo].dbo.T_SYS_USER where ID = Creator) Name,[CreateTime] FROM T_Bridge Where [Key]=@ID   ";
        #endregion
    }
}
