/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using Smartflow.Abstraction.Body;
using Smartflow.Abstraction.DTO;
using Smartflow.Bussiness.Models;
using Smartflow.Core;

namespace Smartflow.Web.Profile
{
    public class SmartflowProfile : AutoMapper.Profile
    {
        public SmartflowProfile()
        {
            CreateMap<Supervise, SummaryDto>();
            CreateMap<Summary, SummaryDto>();

            CreateMap<WorkflowStructureBody, WorkflowStructure>();
            CreateMap<WorkflowStructure, WorkflowStructureDto>();

            CreateMap<Pending, PendingDto>();
            CreateMap<Record, RecordDto>();
            CreateMap<Bridge, BridgeDto>();

            CreateMap<Bridge, BridgeBody>();
            CreateMap<BridgeBody, Bridge>();
            CreateMap<User, UserDto>();
        }
    }
}
