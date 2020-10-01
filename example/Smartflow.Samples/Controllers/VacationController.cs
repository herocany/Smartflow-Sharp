using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;
using Smartflow.Samples.Code;
using Smartflow.Samples.Models;

namespace Smartflow.Samples.Controllers
{
    public class VacationController : ApiController
    {
        private readonly VacationService vacationService = new VacationService();

        public string Post(VacationDto vacation)
        {
            vacation.CreateTime = DateTime.Now;
            vacation.NID = Guid.NewGuid().ToString();
            var model= EmitCore.Convert<VacationDto, Vacation>(vacation);
            vacationService.Insert(model);
            return CommonMethods.Start(vacation.NID, "001001", vacation.UID, String.Format("{0}请假{1}天", vacation.Name, vacation.Day));
        }

        public VacationDto Get(string id)
        {
            return EmitCore.Convert<Vacation, VacationDto>(vacationService.Get(id));
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
