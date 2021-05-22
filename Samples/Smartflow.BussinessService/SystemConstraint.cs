using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.BussinessService
{
    public class SystemConstraint
    {
        public static readonly string CONST_WORKFLOW_START = GlobalObjectService.Configuration.GetSection("Start").Value;
    }
}
