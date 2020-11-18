using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Web.Code;
using Smartflow.Web.Models;

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
        public ResultData Query(Paging paging)
        {
           List<WorkflowStructure> structures =
                _abstractService.WorkflowStructureService.Query(paging.Page, paging.Limit, out int total, paging.Get());
            var w = EmitCore.Convert<List<WorkflowStructure>, List<WorkflowStructureDto>>(structures);
            return CommonMethods.Response(w, total);
        }

        public WorkflowStructureDto Get(string id)
        {
            return EmitCore.Convert<WorkflowStructure, WorkflowStructureDto>(
                _abstractService.WorkflowStructureService.Query(id).FirstOrDefault());
        }

        [HttpPost]
        public void Update(WorkflowStructureRequstDto dto)
        {
            WorkflowStructure model = _abstractService
                .WorkflowStructureService.Query(dto.NID).FirstOrDefault();

            model.Status = dto.Status;

            if (model.Status == 1)
            {
                IList<WorkflowStructure> wfList = _abstractService
                    .WorkflowStructureService.Query().Where(e => e.CategoryCode == model.CategoryCode && e.NID != model.NID && e.Status == 1)
                    .ToList<WorkflowStructure>();

                foreach (WorkflowStructure entry in wfList)
                {
                    entry.Status = 0;
                    _abstractService.WorkflowStructureService.Persistent(entry);
                }
            }
            _abstractService.WorkflowStructureService.Persistent(model);
        }

        public void Post(WorkflowStructureCommandDto dto)
        {
            dto.Resource = Uri.UnescapeDataString(dto.Resource);
            if (!String.IsNullOrEmpty(dto.NID))
            {
                WorkflowStructure model = _abstractService.WorkflowStructureService
                    .Query(dto.NID).FirstOrDefault();
                if (model != null)
                {
                    if (dto.CategoryCode != model.CategoryCode)
                    {
                        dto.Status = 0;
                    }
                }
            }

            var w = EmitCore.Convert<WorkflowStructureCommandDto, WorkflowStructure>(dto);
            w.CreateTime = DateTime.Now;
            _abstractService.WorkflowStructureService.Persistent(w);
        }

        public void Delete(string id)
        {
            _abstractService.WorkflowStructureService.Delete(id);
        }
    }
}