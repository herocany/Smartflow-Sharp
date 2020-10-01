using Smartflow.BussinessService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Smartflow.BussinessService.Services
{
    public class UserService
    {
        public User Get(string id)
        {
            string executeSql = @"SELECT * FROM T_SYS_USER WHERE UID=@UID";
            return DBUtils.CreateConnection().Query<User>(executeSql, new { UID = id }).FirstOrDefault();
        }
    }
}
