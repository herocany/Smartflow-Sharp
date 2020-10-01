using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Web.Code;

namespace Smartflow.Web.Controllers
{
    public class StructureController : ApiController
    {
        private readonly AbstractBridgeService _abstractService;
        public StructureController(AbstractBridgeService abstractService)
        {
            _abstractService = abstractService;
        }

        [HttpPost]
        public dynamic Query(Paging paging)
        {
            IList<WorkflowStructure> structs =
                _abstractService.WorkflowStructureService.Query(paging.Page, paging.Limit, out int total, paging.Get());

            return CommonMethods.Response(structs, total);
        }

        public WorkflowStructure Get(string id)
        {
            return _abstractService.WorkflowStructureService.Query(id).FirstOrDefault();
        }

        public void Put(WorkflowStructure workflowStructure)
        {
            WorkflowStructure model = this.Get(workflowStructure.NID);

            model.Status = workflowStructure.Status;

            if (model.Status == 1)
            {
                IList<WorkflowStructure> wfList = _abstractService
                    .WorkflowStructureService.Query().Where(e => e.CateCode == model.CateCode && e.NID != model.NID && e.Status == 1)
                    .ToList<WorkflowStructure>();

                foreach (WorkflowStructure entry in wfList)
                {
                    entry.Status = 0;
                    _abstractService.WorkflowStructureService.Persistent(entry);
                }
            }
            _abstractService.WorkflowStructureService.Persistent(model);
        }

        public void Post(WorkflowStructure workflowStructure)
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

            _abstractService.WorkflowStructureService.Persistent(workflowStructure);
        }

        public void Delete(string id)
        {
            _abstractService.WorkflowStructureService.Delete(id);
        }
    }
}