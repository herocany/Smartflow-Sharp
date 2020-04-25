using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;

namespace Smartflow.Web.Controllers
{
    public  class StructureController : ApiController
    {
        private readonly AbstractBridgeService _abstractBridgeService;

        public StructureController(AbstractBridgeService abstractBridgeService)
        {
            _abstractBridgeService = abstractBridgeService;
        }


        public dynamic Get()
        {
            IList<WorkflowStructure> structs = _abstractBridgeService.WorkflowStructureService.Query();

            return CommonMethods.Success(structs, structs.Count);
        }

        public WorkflowStructure Get(string id)
        {
            return _abstractBridgeService.WorkflowStructureService.Query(id).FirstOrDefault();
        }
     
        public void Put([FromBody]WorkflowStructure workflowStructure)
        {
            WorkflowStructure model = this.Get(workflowStructure.NID);
          
            model.Status = workflowStructure.Status;

            if (model.Status == 1)
            {
                IList<WorkflowStructure> wfList = _abstractBridgeService
                    .WorkflowStructureService.Query().Where(e => e.CateCode == model.CateCode && e.NID != model.NID && e.Status == 1)
                    .ToList<WorkflowStructure>();

                foreach (WorkflowStructure entry in wfList)
                {
                    entry.Status = 0;
                    _abstractBridgeService.WorkflowStructureService.Persistent(entry);
                }
            }
            _abstractBridgeService.WorkflowStructureService.Persistent(model);
        }

        public void Post([FromBody]WorkflowStructure workflowStructure)
        {
            workflowStructure.StructXml = Uri.UnescapeDataString(workflowStructure.StructXml);
            workflowStructure.CreateDateTime = DateTime.Now;
            WorkflowStructure model = this.Get(workflowStructure.NID);
            if (model != null)
            {
                if (workflowStructure.CateCode != model.CateCode)
                {
                    workflowStructure.Status = 0;
                }
            }

            _abstractBridgeService.WorkflowStructureService.Persistent(workflowStructure);
        }

        public void Delete(string id)
        {
            _abstractBridgeService.WorkflowStructureService.Delete(id);
        }
    }
}