/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Smartflow.Internals
{
    internal partial class ResourceManage
    {
        #region WorkflowInstance
        public const string SQL_WORKFLOW_INSTANCE = "SELECT X.InstanceID, X.Resource, X.State FROM T_INSTANCE X WHERE X.InstanceID = @InstanceID";
        public const string SQL_WORKFLOW_INSTANCE_INSERT = "INSERT INTO T_INSTANCE(InstanceID,State,Resource) VALUES(@InstanceID,@State,@Resource)";
        public const string SQL_WORKFLOW_INSTANCE_UPDATE_RELATIONSHIP= " UPDATE T_INSTANCE SET RelationshipID=@RelationshipID WHERE InstanceID=@InstanceID ";
        public const string SQL_WORKFLOW_LINK = "INSERT INTO T_LINK (NID,InstanceID,RelationshipID) VALUES(@NID,@InstanceID,@RelationshipID) ";
        public const string SQL_WORKFLOW_LINK_DELETE = "DELETE FROM T_LINK WHERE  RelationshipID=@RelationshipID And InstanceID=@InstanceID";
        public const string SQL_WORKFLOW_INSTANCE_UPDATE_TRANSFER = " UPDATE T_INSTANCE SET State=@State WHERE InstanceID=@InstanceID ";
        #endregion
    }
}
