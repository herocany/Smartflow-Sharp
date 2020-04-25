using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IBridgeService: IQuery<Bridge, string>, IQuery<Bridge, Dictionary<string, string>>
    {
    }
}
