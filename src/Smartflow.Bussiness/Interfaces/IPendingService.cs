using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IPendingService 
    {
        IList<Pending> GetPending(string instanceID, string actorID);

        IList<Pending> GetPendingByInstanceID(string instanceID);

        IList<Pending> Query(Dictionary<string, object> queryArg);
    }
}
