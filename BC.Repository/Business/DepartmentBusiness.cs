using BC.Repository.Context;
using BC.Repository.Domain;
using System;
using System.Collections.Generic;

namespace BC.Repository.Business
{
    public class DepartmentBusiness
    {
        private IUnitOfWork<Department> context;

        public DepartmentBusiness(IUnitOfWork<Department> _Context)
        {
            context = _Context;
        }

        public Department GetByIdDepartment(int idDepartment)
        {
            Department department = context.GetById(idDepartment);

            if(department.IdDepartmentParent != null) department.DepartmentParent = context.GetById(department.IdDepartmentParent);

            return department;
        }
    }
}
