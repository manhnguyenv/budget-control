using BC.Repository.Context;
using BC.Repository.Domain;
using System;
using System.Collections.Generic;

namespace BC.Repository.Business
{
    public class ProjectBusiness
    {
        private IUnitOfWork<Project> projectContext;
        private RequestBusiness busRequest;
        private DepartmentBusiness busDepartment;

        public ProjectBusiness(IUnitOfWork<Project> _projectContext, IUnitOfWork<Request> _requestContext, IUnitOfWork<Department> _departmentContext)
        {
            projectContext = _projectContext;
            busRequest = new RequestBusiness(_requestContext);
            busDepartment = new DepartmentBusiness(_departmentContext);
        }

        public Project GetByIdProject(int idProject)
        {
            Project project = projectContext.GetById(idProject);

            if (project != null)
            {
                project.Requests = (ICollection<Request>)busRequest.GetRequestsByIdProject(idProject);
                project.Department = busDepartment.GetByIdDepartment(project.IdDepartment);
            }

            return project;
        }
    }
}
