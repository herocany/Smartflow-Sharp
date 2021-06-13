using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Smartflow.BussinessService.Models;
using Dapper;
using Smartflow.BussinessService.Interfaces;

namespace Smartflow.BussinessService.Services
{
    public class VacationService: IVacationService
    {
        private IDbConnection Connection
        {
            get{ return DBUtils.CreateConnection();}
        }

        private readonly string SQL_COMMAND_INSERT = @"INSERT INTO T_VACATION([NID],[UID],[Name],[Day],[Reason],[CreateTime],[VacationType]) VALUES(@NID,@UID,@Name,@Day,@Reason,@CreateTime,@VacationType)";
        private readonly string SQL_COMMAND_SELECT = @"SELECT * FROM T_VACATION WHERE 1=1  AND NID=@NID  ";

        public void Persistent(Vacation model)
        {
            Connection.Execute(SQL_COMMAND_INSERT, model);
        }
   
        public Vacation GetVacationByID(string id)
        {
            return Connection.Query<Vacation>(SQL_COMMAND_SELECT, new { NID = id }).FirstOrDefault();

        }
    }
}
