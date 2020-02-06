using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Smartflow.BussinessService.Models;
using Dapper;

namespace Smartflow.BussinessService.Services
{
    public class VacationService
    {
        private IDbConnection Connection
        {
            get{ return DBUtils.CreateConnection();}
        }

        private readonly string SQL_COMMAND_INSERT = @"INSERT INTO  T_VACATION([NID],[Name],[Day],[Reason],[CateCode],[CreateDateTime],[VacationType],StartDate,EndDate,[NodeName])
                                                       VALUES(@NID,@Name,@Day,@Reason,@CateCode,@CreateDateTime,@VacationType,@StartDate,@EndDate,@NodeName)";
        private readonly string SQL_COMMAND_UPDATE = @"UPDATE T_VACATION SET [Name]= @Name,[Minute] = @Minute,[Reason] = @Reason,[CateCode] = @CateCode,[CreateDateTime] = @CreateDateTime,[VacationType] = @VacationType,StartDate=@StartDate,EndDate=@EndDate,NodeName=@NodeName WHERE NID=@NID ";
        private readonly string SQL_COMMAND_DELETE = @"DELETE FROM T_VACATION WHERE NID=@NID";
        private readonly string SQL_COMMAND_SELECT = @"SELECT * FROM T_VACATION WHERE 1=1 ";

        public void Insert(Vacation model)
        {
            Connection.Execute(SQL_COMMAND_INSERT, model);
        }

        public void Update(Vacation model)
        {
            Connection.Execute(SQL_COMMAND_UPDATE, model);
        }

        public IList<Vacation> Query()
        {
            return Connection.Query<Vacation>(SQL_COMMAND_SELECT+ " ORDER BY CreateDateTime DESC ").ToList();
        }

        public Vacation Get(string id)
        {
            string command = SQL_COMMAND_SELECT + " AND NID=@NID ";
            return Connection.Query<Vacation>(command, new { NID = id }).FirstOrDefault();
        }

        public void Delete(string id)
        {
            Connection.Execute(SQL_COMMAND_DELETE, new { NID = id });
        }
    }
}
