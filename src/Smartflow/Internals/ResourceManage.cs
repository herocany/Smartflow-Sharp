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
        /// <summary>
        /// 验证MAIL地址，正则表达式
        /// </summary>
        public const string MAIL_URL_EXPRESSION = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
    }
}
