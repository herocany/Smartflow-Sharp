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
        #region WorkflowStructureService
        public const string SQL_WORKFLOW_STRUCTURE_INSERT = " INSERT INTO T_Structure(StructName,StructXml,CateCode,CateName,Memo,Status,CreateDateTime) VALUES(@StructName,@StructXml,@CateCode,@CateName,@Memo,@Status,@CreateDateTime)";
        public const string SQL_WORKFLOW_STRUCTURE_UPDATE = " UPDATE T_Structure SET StructName=@StructName,CateCode=@CateCode,CateName=@CateName,StructXml=@StructXml,Memo=@Memo,Status=@Status WHERE NID=@NID";
        public const string SQL_WORKFLOW_STRUCTURE_DELETE = " DELETE FROM T_Structure WHERE NID=@NID ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT_ID = " SELECT * FROM T_Structure WHERE NID=@NID  ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT = " SELECT * FROM T_Structure ORDER BY CreateDateTime DESC  ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT_PAGING = " SELECT TOP {0} *,(SELECT Name FROM [dbo].[T_CATEGORY] WHERE NID = CateCode) CateName FROM T_Structure WHERE NID NOT IN (SELECT TOP {1} NID FROM T_Structure WHERE 1=1 {2} ORDER BY CreateDateTime Desc ) {2}  ORDER BY CreateDateTime Desc  ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT_TOTAL = " SELECT COUNT(1) FROM T_Structure WHERE 1=1 {0}  ";
        #endregion
    }
}
