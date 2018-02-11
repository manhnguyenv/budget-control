using BC.Domain;
using BC.ViewModel.Common;
using BC.ViewModel.Resultset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Contracts.Services
{
    public interface IManagementService
    {
        BarChartResultsetVM GetBudget(int id, int year);

        PieChartResultsetVM GetSupplier(int id, int year);

        ManagementCommonVM GetPendingRequest(int id, int year);

        ManagementCommonVM GetGeneralInformation(int? year);
    }
}
