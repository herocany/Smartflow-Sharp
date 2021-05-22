using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartflow.Samples.Models
{
    public class VacationDto
    {
        public string NID
        {
            get;
            set;
        }


        public string UID { get; set; }

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

        public DateTime CreateTime
        {
            get;
            set;
        }



        public string VacationType
        {
            get;
            set;
        }
    }
}
