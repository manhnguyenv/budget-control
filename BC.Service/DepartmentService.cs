using System;
using System.Collections.Generic;
using BC.Contracts.Repository;
using BC.Contracts.Services;
using BC.Domain;
using System.Linq;

namespace BC.Service
{
    public class DepartmentService : IDepartmentService
    {
        private IUnitOfWork context;

        public DepartmentService(IUnitOfWork _context)
        {
            context = _context;
        }

        public Department GetByIdDepartment(int idDepartment)
        {
            Department department = context.DepartmentRepository.GetById(idDepartment);

            if(department.IdDepartmentParent != null) department.DepartmentParent = context.DepartmentRepository.GetById(department.IdDepartmentParent);

            return department;
        }

        public List<Department> GetAllDepartments()
        {
            IEnumerable<Department> departments = context.DepartmentRepository.GetAll();
            IEnumerable<Department> parents = departments.Where(e => e.IdDepartmentParent == null).OrderBy(o => o.Description);

            List<Department> response = new List<Department>();
            foreach(Department item in parents)
            {
                response.Add(item);
                response.AddRange(departments.Where(e => e.IdDepartmentParent == item.Id).OrderBy(o => o.Description));
            }

            return response;
        }
    }
}
