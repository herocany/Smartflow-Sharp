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

        #endregion


    }
}
