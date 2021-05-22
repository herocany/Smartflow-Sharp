using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IBridgeService 
    {
        Bridge GetBridge(string id);

        Bridge GetBridgeByInstanceID(string instanceID);

        Bridge Query(Dictionary<string, string> queryArg);
    }
}
