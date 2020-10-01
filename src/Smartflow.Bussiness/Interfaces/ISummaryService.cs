using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface ISummaryService:IQuery<IList<Summary>, Dictionary<string, string>>, IPagingQuery<IList<Summary>, Paging>
    {
        IList<Summary> QuerySupervise(Paging info, out int total);
    }
}
