using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Common;
using Smartflow.Web.Models;

namespace Smartflow.Web.Controllers
{
    public class PendingController : ApiController
    {
        private readonly IPendingService _pendingService;
        private readonly IQuery<IList<Category>> _categoryService;

        public PendingController(IPendingService pendingService, IQuery<IList<Category>> categoryService)
        {
            _pendingService = pendingService;
            _categoryService = categoryService;
        }

        public IEnumerable<Pending> Get(string id)
        {
            return _pendingService.Query(id);
        }

        public void Delete(PendingDeleteDto dto)
        {
            string[] ids = dto.ActorIDs.Split(',');
            if (ids.Length > 0)
            {
                WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);
                var node = instance.Current.FirstOrDefault(e => e.ID == dto.NodeID);
                foreach (string actor in ids)
                {
                    CommandBus.Dispatch<Dictionary<string, object>>(new DeletePendingByActor(), new Dictionary<string, object>
                    {
                        { "instanceID", dto.ID },
                        { "nodeID", node.NID},
                        { "actorID", actor }
                    });
                }
            }
        }

        public void Post(PendingCommandDto dto)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);
            var node = instance.Current.FirstOrDefault(e => e.ID== dto.NodeID);
            Category model = _categoryService.Query().FirstOrDefault(cate => cate.NID == dto.CategoryCode);
            string[] ids = dto.ActorIDs.Split(',');
            foreach (string id in ids)
            {
                CommandBus.Dispatch<Pending>(new CreatePending(), new Pending
                {
                    NID = Guid.NewGuid().ToString(),
                    ActorID = id,
                    InstanceID = instance.InstanceID,
                    NodeID = node.NID,
                    Url = model.Url,
                    CreateTime = DateTime.Now,
                    NodeName = node.Name,
                    CategoryCode = dto.CategoryCode,
                    CategoryName = model.Name
                });
            }
        }
    }
}