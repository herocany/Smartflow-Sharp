using AutoMapper;
using Smartflow.Bussiness.Models;
using Smartflow.Core;
using Smartflow.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartflow.Web.Profile
{
    public class SmartflowProfile: AutoMapper.Profile
    {
        public SmartflowProfile()
        {
            CreateMap<Supervise, SummaryDto>();
            CreateMap<Summary, SummaryDto>();
            CreateMap<WorkflowStructureCommandDto, WorkflowStructure>();
            CreateMap<WorkflowStructure, WorkflowStructureDto>();

            CreateMap<Pending, PendingDto>();
            CreateMap<Record, RecordDto>();
            CreateMap<Bridge, BridgeDto>();

            CreateMap<Bridge, BridgeCommandDto>();
            CreateMap<BridgeCommandDto, Bridge>();
            CreateMap<User, UserDto>();
        }
    }
}
