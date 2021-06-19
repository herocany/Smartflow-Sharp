/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Net;

namespace Smartflow.API.Code
{
    public class CommonMethods
    {
        public static ResultData Response(Object data, int total)
        {
            return new ResultData
            {
                Code = 200,
                Total = total,
                Data=data
            };
        }
    }
}