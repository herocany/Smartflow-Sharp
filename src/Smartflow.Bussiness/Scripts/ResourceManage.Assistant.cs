using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region    
        public const string SQL_ASSISTANT_INSERT = @"INSERT INTO [dbo].[t_assistant](NID,InstanceID,NodeID,Total) SELECT NEWID(),InstanceID,NodeID, [dbo].[GetAssistant](InstanceID,NodeID) FROM (SELECT InstanceID,NodeID FROM [dbo].[t_pending] WHERE NodeID NOT IN (SELECT NodeID  FROM [dbo].[t_assistant] WHERE [InstanceID]=@InstanceID)  AND InstanceID=@InstanceID GROUP BY InstanceID,NodeID) T ";
        #endregion
    }
}
