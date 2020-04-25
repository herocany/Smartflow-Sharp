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

        public static string BindQuot(string ids)
        {
            string[] RArry = ids.Split(',');
            string[] NRArray = new string[RArry.Length];
            for (int i = 0; i < RArry.Length; i++)
            {
                NRArray[i] = string.Format("'{0}'", RArry[i]);
            }
            return string.Join(",", NRArray);
        }
    }
}
