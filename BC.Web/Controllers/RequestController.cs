using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BC.Repository.Domain;
using BC.Repository.Business;
using BC.Repository.Context;

namespace BC.Web.Controllers
{
    public class RequestController : Controller
    {
        private ProjectBusiness projectBus;
        private RequestBusiness requestBus;

        public RequestController(IUnitOfWork<Project> _projectContext, IUnitOfWork<Request> _requestContext, IUnitOfWork<Department> _departmentContext)
        {
            projectBus = new ProjectBusiness(_projectContext, _requestContext, _departmentContext);
            requestBus = new RequestBusiness(_requestContext);
        }

        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchProject(int pId)
        {
            return PartialView("_RequestGrid", projectBus.GetByIdProject(pId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRequest([Bind(Include ="Id, IdProject, IdSupplier, Value")] Request request)
        {
            requestBus.EditRequest(request);
            return PartialView("_RequestGrid", projectBus.GetByIdProject(request.IdProject));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequest([Bind(Include = "IdProject, IdSupplier, Value")]Request request)
        {
            requestBus.CreateRequest(request);
            return PartialView("_RequestGrid", projectBus.GetByIdProject(request.IdProject));
        }

        public JsonResult GetRequestByID(int? Id)
        {
            Request request = requestBus.GetRequestById(Id);
            return new JsonResult
            {
                Data = request,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}