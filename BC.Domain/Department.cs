using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Domain
{
    public class Department : Base
    {
        public Department()
        {
            Departments = new HashSet<Department>();
        }

        public string Description { get; set; }
        public int? IdDepartmentParent { get; set; }

        public virtual Department DepartmentParent { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
