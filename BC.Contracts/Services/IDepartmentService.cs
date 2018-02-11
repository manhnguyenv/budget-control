using BC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Contracts.Services
{
    public interface IDepartmentService
    {
        Department GetByIdDepartment(int idDepartment);

        List<Department> GetAllDepartments();
    }
}
