using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region CreatePending\PendingQueryService\DeletePendingByActor
        public const string SQL_PENDING_INSERT = @"INSERT INTO T_PENDING(NID,ActorID,NodeID,InstanceID,NodeName,CateCode,CateName,Url,CreateDateTime) VALUES(@NID,@ActorID,@NodeID,@InstanceID,@NodeName,@CateCode,@CateName,@Url,@CreateDateTime)";
        public const string SQL_PENDING_SELECT = @" SELECT * FROM T_PENDING WHERE ActorID=@ActorID Order by CreateDateTime Desc ";
        public const string SQL_PENDING_SELECT_1 = @" SELECT * FROM T_PENDING WHERE InstanceID=@InstanceID AND ActorID=@ActorID AND NodeID=@NodeID ";
        public const string SQL_PENDING_DELETE = @" DELETE FROM T_PENDING WHERE InstanceID=@InstanceID ";
        public const string SQL_PENDING_DELETE_1 = @" DELETE FROM T_PENDING WHERE InstanceID=@InstanceID AND NodeID=@NodeID ";
        public const string SQL_PENDING_DELETE_2 = @" DELETE FROM T_PENDING WHERE InstanceID=@InstanceID AND NodeID=@NodeID AND ActorID=@ActorID ";
        #endregion
    }
}
