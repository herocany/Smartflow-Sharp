using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;

namespace Smartflow.Web.Controllers
{
    public class VacationController : ApiController
    {
        private readonly VacationService vacationService = new VacationService();

        public string Post(Vacation vacation)
        {
            vacation.Day = vacation.EndDate.Subtract(vacation.StartDate).Days;
            string key = (String.IsNullOrEmpty(vacation.NID)) ? Guid.NewGuid().ToString() : vacation.NID;
            if (String.IsNullOrEmpty(vacation.NID))
            {
                vacation.CreateDateTime = DateTime.Now;
                vacation.NID = key;
                vacation.NodeName = "开始";
                vacationService.Insert(vacation);
            }
            else
            {
                vacationService.Update(vacation);
            }
            return key;
        }

        public dynamic Get()
        {
            IList<Vacation> list = vacationService.Query();
            return VacationController.Success(list, list.Count);
        }

        public Vacation Get(string id)
        {
            return vacationService.Get(id);
        }

        public void Delete(string id)
        {
            vacationService.Delete(id);
        }

        public void Put(Vacation vacation)
        {
            vacationService.Update(vacation);
        }

        public static dynamic Success(Object data, int total)
        {
            return new
            {
                code = 0,
                msg = "操作成功",
                count = total,
                data
            };
        }
    }
}
