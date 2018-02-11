using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.Domain;

namespace BC.ViewModel.Resultset
{
    public class ManagementResultsetVM
    {
        public BarChartResultsetVM GeneralBudget { get; set; }
        public PieChartResultsetVM GeneralSuppliers { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IDictionary<int, int> PendingDepartments { get; set; }
        public IEnumerable<Request> PendingRequests { get; set; }

        public bool isPendingDepartment(int id)
        {
            return PendingDepartments.ContainsKey(id);
        }
    }
}
