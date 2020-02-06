using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.BussinessService.Models
{
    public class Vacation
    {
        public string NID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Day
        {
            get;
            set;
        }

        public string Reason
        {
            get;
            set;
        }
     
        public string CateCode
        {
            get;
            set;
        }

        public DateTime CreateDateTime
        {
            get;
            set;
        }

        public DateTime EndDate
        {
            get;
            set;
        }

        public DateTime StartDate
        {
            get;
            set;
        }

        public string VacationType
        {
            get;
            set;
        }

        public String NodeName
        {
            get;
            set;
        }
    }
}
