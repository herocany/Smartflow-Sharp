using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface ISummaryService:IPagingQuery<IList<Summary>, Paging>
    {
        IList<Summary> Query(Dictionary<string, string> queryArg);

        IList<Supervise> QuerySupervise(Paging info, out int total);
    }
}
