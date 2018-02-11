using BC.Domain;
using BC.ViewModel.Common;
using BC.ViewModel.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Contracts.Services
{
    public interface IRequestService
    {
        IEnumerable<Request> GetRequestsByIdProject(int idProject);
        //Request GetRequestById(int? id);
        RequestCommonVM CreateRequest(RequestRegisterVM requestToCreate);
        //int EditRequest(Request dataToEdit);
        RequestCommonVM CancelRequest(RequestRegisterVM requestToCreate);
        RequestCommonVM ApproveRequest(int Id, bool approve);
    }
}
