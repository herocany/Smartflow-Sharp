using AutoMapper;
using Smartflow.BussinessService.Models;
using Smartflow.Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Smartflow.Samples.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vacation, VacationDto>();
            CreateMap<VacationDto, Vacation>();
        }
    }
}
