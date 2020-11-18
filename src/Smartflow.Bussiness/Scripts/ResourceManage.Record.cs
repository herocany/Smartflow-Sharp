using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region CreateRecord\RecordQueryService
        public const string SQL_RECORD_SELECT = @" SELECT * FROM T_RECORD WHERE InstanceID=@InstanceID  ORDER BY CreateTime ";
        public const string SQL_RECORD_INSERT = @" INSERT INTO T_RECORD([NID],[Name],[NodeID],[Comment],[InstanceID],[CreateTime],[Url],[AuditUserID],[AuditUserName]) 
                                                       VALUES (@NID,@Name,@NodeID,@Comment,@InstanceID,@CreateTime,@Url,@AuditUserID,@AuditUserName)";

        #endregion
    }
}
