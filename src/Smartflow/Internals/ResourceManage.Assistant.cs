using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow.Internals
{
    internal partial class ResourceManage
    {
        #region    
        public const string SQL_ASSISTANT_SELECT_COUNT = @"SELECT Total FROM [dbo].[t_assistant] WHERE NodeID=@NodeID AND InstanceID=@InstanceID ";
        #endregion
    }
}
