using Smartflow.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IRecordService
    {
        IList<Record> GetRecordByInstanceID(string instanceID);
    }
}
