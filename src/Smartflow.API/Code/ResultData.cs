/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;

namespace Smartflow.API.Code
{
    public class ResultData
    {
        public ResultData()
        {

        }

        public ResultData(int code, string message, Object data = null)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public ResultData(int code, string message, Object data, int total)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
            this.Total = total;
        }

        public int Code
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public Object Data
        {
            get;
            set;
        }
    }
}
