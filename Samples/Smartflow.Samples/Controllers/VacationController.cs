using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;
using Smartflow.Samples.Models;

namespace Smartflow.Samples.Controllers
{
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly VacationService vacationService = new VacationService();
        private readonly IMapper _mapper;
        public VacationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("api/vacation/persistent"), HttpPost]
        public string Post(VacationDto vacation)
        {
            vacation.CreateTime = DateTime.Now;
            vacation.NID = Guid.NewGuid().ToString();
            var model = _mapper.Map<VacationDto, Vacation>(vacation);
            vacationService.Insert(model);
           return CommonMethods.Start(vacation.NID, "001001", vacation.UID,String.Format("{0}请假{1}天", vacation.Name, vacation.Day));
        }

        [Route("api/vacation/{id}/info"), HttpGet]
        public VacationDto Get(string id)
        {
            return _mapper.Map<Vacation, VacationDto>(vacationService.Get(id));
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
