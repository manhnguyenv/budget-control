using BC.Contracts.Repository;
using BC.Contracts.Services;
using BC.Domain;
using BC.ViewModel.Common;
using BC.ViewModel.Resultset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Service
{
    public class ManagementService : IManagementService
    {
        private IUnitOfWork unit;
        private IDepartmentService busDepartment;

        public ManagementService(IUnitOfWork _unit, IDepartmentService _busDepartment)
        {
            unit = _unit;
            busDepartment = _busDepartment;
        }

        private BarChartResultsetVM GenerateBarChart(bool general, double budget, double approved, double pending, int year)
        {
            BarChartResultsetVM response = new BarChartResultsetVM();

            response.configuration.year = year;
            response.configuration.titleColor = (general) ? "blue" : "orange";
            response.configuration.icon = "bar chart icon";
            response.configuration.title = (general) ? "General Budget Info" : "Department Budget Info" ;
            response.configuration.subtitle = "Budget x Approved & Pending requests";

            response.data = new ViewModel.Chart.BarChart()
            {
                labels = new string[0],
                datasets = new ViewModel.Chart.BarDataset[3]
                {
                    new ViewModel.Chart.BarDataset()
                    {
                        stack = "s0",
                        borderWidth = 1,
                        backgroundColor = "rgba(183,186,190,0.5)",
                        borderColor = "rgba(183,186,190,1)",
                        data = new double[1] { budget },
                        label = "Budget"
                    },
                    new ViewModel.Chart.BarDataset()
                    {
                        stack = "s1",
                        borderWidth = 1,
                        backgroundColor = "rgba(17,159,27,0.5)",
                        borderColor = "rgba(17,159,27,1)",
                        data = new double[1] { approved },
                        label = "Approved Requests"
                    },
                    new ViewModel.Chart.BarDataset()
                    {
                        stack = "s1",
                        borderWidth = 1,
                        backgroundColor = "rgba(215,227,29,0.5)",
                        borderColor = "rgba(215,227,29,1)",
                        data = new double[1] { pending },
                        label = "Pending Requests"
                    }
                }
            };

            return response;
        }

        private PieChartResultsetVM GeneratePieChart(bool general, string[] suppliers, string[] background, string[] border, double[] values, int year)
        {
            PieChartResultsetVM response = new PieChartResultsetVM();

            response.configuration.year = DateTime.Now.Year;
            response.configuration.titleColor = (general) ? "blue" : "orange";
            response.configuration.icon = "pie chart icon";
            response.configuration.title = (general) ? "General Suppliers Info" : "Department Suppliers Info";
            response.configuration.subtitle = "Requests per supplier";

            response.data = new ViewModel.Chart.PieChart()
            {
                labels = suppliers,
                datasets = new ViewModel.Chart.PieDataset[1]
                {
                    new ViewModel.Chart.PieDataset()
                    {
                        borderWidth = 1,
                        backgroundColor = background,
                        borderColor = border,
                        data = values
                    }
                }
            };

            return response;
        }

        private Dictionary<int, int> GetPendingRequestsAllDepartment(int year)
        {
            Dictionary<int, int> pending = new Dictionary<int, int>();

            var query = unit.RequestRepository
                .Where(e => e.RequestStatus == Status.Pending && e.RequestDate.Year == year, n => n.Project)
                .GroupBy(g => g.Project.IdDepartment)
                .Select(s => new
                {
                    Key = s.FirstOrDefault().Project.IdDepartment,
                    Quantity = s.Count()
                }).ToList();

            foreach(var item in query)
            {
                pending.Add(item.Key, item.Quantity);
            }

            return pending;
        }

        public BarChartResultsetVM GetBudget(int id, int year)
        {
            double budget = 0;
            double approved = 0;
            double pending = 0;

            if (id == 0)
            {
                var status = unit.RequestRepository
                .Where(e => e.RequestDate.Year == year)
                .GroupBy(g => g.RequestStatus)
                .Select(s => new {
                    Key = s.FirstOrDefault().RequestStatus,
                    Sum = s.Sum(v => v.Value)
                }).ToList();

                if(status.FirstOrDefault(f => f.Key == Status.Approved) != null) approved = (double)status.FirstOrDefault(f => f.Key == Status.Approved).Sum;
                if (status.FirstOrDefault(f => f.Key == Status.Pending) != null) pending = (double)status.FirstOrDefault(f => f.Key == Status.Pending).Sum;

                budget = unit.BudgetRepository.Where(e => e.Year == year).Sum(s => (double?)s.Value) ?? 0;
            }
            else
            {
                var status = unit.RequestRepository
                .Where(e => e.RequestDate.Year == year && e.Project.IdDepartment == id)
                .GroupBy(g => g.RequestStatus)
                .Select(s => new {
                    Key = s.FirstOrDefault().RequestStatus,
                    Sum = s.Sum(v => v.Value)
                }).ToList();

                if (status.FirstOrDefault(f => f.Key == Status.Approved) != null) approved = (double)status.FirstOrDefault(f => f.Key == Status.Approved).Sum;
                if (status.FirstOrDefault(f => f.Key == Status.Pending) != null) pending = (double)status.FirstOrDefault(f => f.Key == Status.Pending).Sum;

                budget = unit.BudgetRepository.Where(e => e.Year == year && e.IdDepartment == id).Sum(s => (double?)s.Value) ?? 0;
            }

            return GenerateBarChart(id == 0, budget, approved, pending, year);
        }

        public PieChartResultsetVM GetSupplier(int id, int year)
        {
            string[] suppliers;
            string[] background = new string[5] {
                            "rgba(29, 95, 227, 0.5)",
                            "rgba(16, 169, 60, 0.5)",
                            "rgba(136, 29, 243, 0.5)",
                            "rgba(216, 186, 19, 0.5)",
                            "rgba(183, 186, 190, 0.5)"
                        };
            string[] border = new string[5] {
                            "rgba(29, 95, 227, 1)",
                            "rgba(16, 169, 60, 1)",
                            "rgba(136, 29, 243, 1)",
                            "rgba(216, 186, 19, 1)",
                            "rgba(183, 186, 190, 1)"
                        }; ;
            double[] values;

            List<string> lstSuppliers = new List<string>();
            List<double> lstValues = new List<double>();

            if (id == 0)
            {
                var query = unit.RequestRepository
               .Where(e => e.RequestDate.Year == year, n => n.Supplier)
               .GroupBy(g => g.Supplier)
               .Select(s => new {
                   Key = s.FirstOrDefault().Supplier.Name,
                   Sum = s.Sum(v => v.Value)
               }).ToList();

                foreach (var item in query)
                {
                    lstSuppliers.Add(item.Key);
                    lstValues.Add((double)item.Sum);
                }
            }
            else
            {
                var query = unit.RequestRepository
               .Where(e => e.RequestDate.Year == year && e.Project.IdDepartment == id, n => n.Supplier)
               .GroupBy(g => g.Supplier)
               .Select(s => new {
                   Key = s.FirstOrDefault().Supplier.Name,
                   Sum = s.Sum(v => v.Value)
               }).ToList().OrderBy(o => o.Sum);

                foreach (var item in query)
                {
                    if (lstSuppliers.Count == 5) break;
                    lstSuppliers.Add(item.Key);
                    lstValues.Add((double)item.Sum);
                }
            }

            suppliers = lstSuppliers.ToArray();
            values = lstValues.ToArray();
            
            return GeneratePieChart(id == 0, suppliers, background, border, values, year);
        }

        public ManagementCommonVM GetPendingRequest(int id, int year)
        {
            ManagementCommonVM response = new ManagementCommonVM();
            List<Request> lstPending = new List<Request>();

            var query = unit.RequestRepository
                .Where(e => e.RequestStatus == Status.Pending &&
                e.RequestDate.Year == year &&
                e.Project.IdDepartment == id, n => n.Project, n => n.Supplier, n => n.RequestsHistoric);

            foreach(var item in query.ToList())
            {
                Request req = new Request()
                {
                    Id = item.Id,
                    IdProject = item.IdProject,
                    IdSupplier = item.IdSupplier,
                    Project = item.Project,
                    RequestDate = item.RequestDate,
                    RequestsHistoric = item.RequestsHistoric,
                    RequestStatus = item.RequestStatus,
                    Supplier = item.Supplier,
                    Value = item.Value
                };

                lstPending.Add(req);
            }

            response.Resultset.PendingRequests = lstPending;

            return response;
        }

        public ManagementCommonVM GetGeneralInformation(int? year)
        {
            ManagementCommonVM response = new ManagementCommonVM();
            response.Resultset.Departments = busDepartment.GetAllDepartments();
            response.SelectedYear = (year == null) ? DateTime.Now.Year : (int)year;
            
            response.Resultset.PendingDepartments = GetPendingRequestsAllDepartment(response.SelectedYear);

            response.Resultset.GeneralBudget = GetBudget(0, response.SelectedYear);
            response.Resultset.GeneralSuppliers = GetSupplier(0, response.SelectedYear);

            return response;
        }
    }
}
