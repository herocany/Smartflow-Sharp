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
        public const string SQL_WORKFLOW_STRUCTURE_INSERT = " INSERT INTO T_Structure(Name,Resource,CategoryCode,CategoryName,Memo,Status,CreateTime) VALUES(@Name,@Resource,@CategoryCode,@CategoryName,@Memo,@Status,@CreateTime)";
        public const string SQL_WORKFLOW_STRUCTURE_UPDATE = " UPDATE T_Structure SET Name=@Name,CategoryCode=@CategoryCode,CategoryName=@CategoryName,Resource=@Resource,Memo=@Memo,Status=@Status WHERE NID=@NID";
        public const string SQL_WORKFLOW_STRUCTURE_DELETE = " DELETE FROM T_Structure WHERE NID=@NID ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT_ID = " SELECT * FROM T_Structure WHERE NID=@NID  ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT = " SELECT * FROM T_Structure ORDER BY CreateTime DESC  ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT_PAGING = " SELECT TOP {0} *,(SELECT Name FROM [dbo].[T_CATEGORY] WHERE NID = CategoryCode) CategoryName FROM T_Structure WHERE NID NOT IN (SELECT TOP {1} NID FROM T_Structure WHERE 1=1 {2} ORDER BY CreateTime Desc ) {2}  ORDER BY CreateTime Desc  ";
        public const string SQL_WORKFLOW_STRUCTURE_SELECT_TOTAL = " SELECT COUNT(1) FROM T_Structure WHERE 1=1 {0}  ";
        #endregion
    }
}
