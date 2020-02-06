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
        public User Get(string userName)
        {
            string executeSql = @"SELECT * FROM T_USER WHERE UserName=@UserName";
            return DBUtils.CreateConnection().Query<User>(executeSql, new { UserName = userName }).FirstOrDefault();
        }
    }
}
