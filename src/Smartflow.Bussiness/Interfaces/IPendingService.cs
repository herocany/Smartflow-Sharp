using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IPendingService : IQuery<IList<Pending>, string>, IQuery<IList<Pending>, Dictionary<string, object>>
    {
    }
}
