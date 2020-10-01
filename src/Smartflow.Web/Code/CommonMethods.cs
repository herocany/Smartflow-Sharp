using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Smartflow.Web.Code
{
    public class CommonMethods
    {
        public static ResultData Response(Object data, int total)
        {
            return new ResultData
            {
                Code = (int)HttpStatusCode.OK,
                Message = "操作成功",
                Total = total,
                Data=data
            };
        }

        public static ResultData Response(int code = (int)HttpStatusCode.OK, Object data = null, String message = null)
        {
            return new ResultData
            {
                Code = code,
                Message = message,
                Data = data
            };
        }

        public static ResultData Response(ResultData result,Object data=null)
        {
            result.Data = data;
            return result;
        }
    }
}