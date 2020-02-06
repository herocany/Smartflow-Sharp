using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    public   class CommonMethods
    {
        public static dynamic Success(Object data, int total)
        {
            return new
            {
                code = 0,
                msg = "操作成功",
                count = total,
                data
            };
        }
    }
}
