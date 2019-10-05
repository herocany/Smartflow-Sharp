using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using Smartflow.BussinessService.Models;

namespace Smartflow.BussinessService.Services
{
    public class FileApplyService : RepositoryService<FileApply>
    {
        public void Persistent(FileApply model)
        {
            Insert(model);
        }

        public FileApply GetByInstanceID(string instanceID)
        {
            return
                base
                .Query(e => e.INSTANCEID == instanceID)
                .FirstOrDefault();
        }


        public DataTable GetFileApplyList()
        {
            DataTable dt = new DataTable();
            using (IDataReader dr = base.Connection.ExecuteReader("SELECT T.*,(select State from Smartflow.dbo.t_instance i Where i.InstanceID=T.InstanceID) State FROM T_APPLY T"))
            {
                dt.Load(dr);
            }
            return dt;
        }
    }
}