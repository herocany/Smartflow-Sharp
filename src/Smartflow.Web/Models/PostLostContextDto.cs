using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Web.Models
{
    public class PostLostContextDto
    {
        public string ID
        {
            get;
            set;
        }

        public string Destination
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }


        public dynamic Data
        {
            get;
            set;
        }
    }
}