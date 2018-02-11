using BC.Contracts.Repository;
using BC.Domain;
using BC.Contracts.Services;
using System;
using System.Collections.Generic;
using BC.ViewModel.Common;
using System.Linq;

namespace BC.Service
{
    public class ProjectService : IProjectService
    {
        private IUnitOfWork context;
        private IRequestService busRequest;
        private IDepartmentService busDepartment;
        private ISupplierService busSupplier;

        public ProjectService(IUnitOfWork _context, IRequestService _requestService, IDepartmentService _departmentService, ISupplierService _supplierService)
        {
            context = _context;
            busRequest = _requestService;
            busDepartment = _departmentService;
            busSupplier = _supplierService;
        }

        public RequestCommonVM Start()
        {
            RequestCommonVM requestMv = new RequestCommonVM();
            requestMv.Code = "";
            requestMv.Resultset.Supplies = busSupplier.GetAllSuppliers();
            return requestMv;
        }

        public RequestCommonVM GetByIdProject(int idProject)
        {
            Project project = context.ProjectRepository.GetById(idProject);

            if (project != null)
            {
                project.Requests = busRequest.GetRequestsByIdProject(idProject).ToList();
                project.Department = busDepartment.GetByIdDepartment(project.IdDepartment);
            }

            RequestCommonVM requestVM = new RequestCommonVM();
            requestVM.Resultset.Project = project;
            requestVM.Resultset.Supplies = busSupplier.GetAllSuppliers();

            return requestVM;
        }
    }
}
