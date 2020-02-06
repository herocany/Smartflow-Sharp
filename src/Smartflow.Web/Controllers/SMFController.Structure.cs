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
        private readonly BaseBridgeService baseBridgeService = new BaseBridgeService();

        public dynamic Get()
        {
            IList<WorkflowStructure> structs = baseBridgeService.WorkflowStructureService.Query();

            return CommonMethods.Success(structs, structs.Count);
        }

        public WorkflowStructure Get(string id)
        {
            return baseBridgeService.WorkflowStructureService.Query(id).FirstOrDefault();
        }
     
        public void Put([FromBody]WorkflowStructure workflowStructure)
        {
            WorkflowStructure model = this.Get(workflowStructure.NID);
          
            model.Status = workflowStructure.Status;

            if (model.Status == 1)
            {
                IList<WorkflowStructure> wfList = baseBridgeService
                    .WorkflowStructureService.Query().Where(e => e.CateCode == model.CateCode && e.NID != model.NID && e.Status == 1)
                    .ToList<WorkflowStructure>();

                foreach (WorkflowStructure entry in wfList)
                {
                    entry.Status = 0;
                    baseBridgeService.WorkflowStructureService.Persistent(entry);
                }
            }
            baseBridgeService.WorkflowStructureService.Persistent(model);
        }

        public void Post([FromBody]WorkflowStructure workflowStructure)
        {
            workflowStructure.StructXml = Uri.UnescapeDataString(workflowStructure.StructXml);
            workflowStructure.CreateDateTime = DateTime.Now;
            baseBridgeService.WorkflowStructureService.Persistent(workflowStructure);
        }

        public void Delete(string id)
        {
           baseBridgeService.WorkflowStructureService.Delete(id);
        }
    }
}