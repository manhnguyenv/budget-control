using BC.Contracts.Repository;
using BC.Domain;
using BC.Contracts.Services;
using System;
using System.Collections.Generic;
using BC.ViewModel.Register;
using BC.ViewModel.Common;

namespace BC.Service
{
    public class RequestService : IRequestService
    {
        private IUnitOfWork context;
        private SupplierService busSupplier;

        private RequestCommonVM CreateInitialInstance()
        {
            RequestCommonVM requestMv = new RequestCommonVM();
            requestMv.Resultset.Supplies = busSupplier.GetAllSuppliers();
            return requestMv;
        }

        public RequestService(IUnitOfWork _context, SupplierService _busSupplier)
        {
            context = _context;
            busSupplier = _busSupplier;
        }

        public IEnumerable<Request> GetRequestsByIdProject(int idProject)
        {
            return context.RequestRepository.Where(r => r.IdProject.Equals(idProject), n => n.Project, n => n.Supplier);
        }

        public RequestCommonVM CreateRequest(RequestRegisterVM requestToCreate)
        {
            RequestCommonVM requestMv = CreateInitialInstance();

            try
            {
                Request request = new Request()
                {
                    RequestStatus = Status.Pending,
                    RequestDate = DateTime.Now,
                    IdProject = requestToCreate.IdProject,
                    IdSupplier = requestToCreate.IdSupplier,
                    Value = requestToCreate.Value
                };
                context.RequestRepository.Save(request);
                
                RequestHistoric historic = new RequestHistoric();
                historic.IdRequest = request.Id;
                historic.RequestDate = request.RequestDate;
                historic.RequestStatus = request.RequestStatus;
                context.RequestHistoricRepository.Save(historic);

                context.Commit();

                requestMv.IsOk = true;
                requestMv.Code = "C_R_S_OK";
                requestMv.Message = "Request created sucessfully";
                return requestMv;
            }
            catch(Exception ex)
            {
                requestMv.IsOk = false;
                requestMv.Code = "C_R_S_ERR";
                requestMv.ErrorMessage = ex.Message;
                requestMv.Ex = ex;
                return requestMv;
            }
        }

        public RequestCommonVM CancelRequest(RequestRegisterVM requestToCancel)
        {
            RequestCommonVM requestMv = CreateInitialInstance();

            try
            {
                Request request = ChangeRequestStatus(requestToCancel.Id, Status.Cancelled);

                RequestHistoric historic = new RequestHistoric();
                historic.IdRequest = request.Id;
                historic.RequestDate = request.RequestDate;
                historic.RequestStatus = request.RequestStatus;
                context.RequestHistoricRepository.Save(historic);

                context.Commit();

                requestMv.IsOk = true;
                requestMv.Code = "CAN_R_S_OK";
                requestMv.Message = "Request cancelled sucessfully";
                return requestMv;
            }
            catch (Exception ex)
            {
                requestMv.IsOk = false;
                requestMv.Code = "CAN_R_S_ERR";
                requestMv.ErrorMessage = ex.Message;
                requestMv.Ex = ex;
                return requestMv;
            }
        }

        public RequestCommonVM ApproveRequest(int Id, bool approve)
        {
            RequestCommonVM response = new RequestCommonVM();

            try
            {
                Request req = ChangeRequestStatus(Id, (approve) ? Status.Approved : Status.Recused);

                context.Commit();

                response.IsOk = true;
                response.Code = "APP_R_OK";
                response.Message = "Request approved sucessfully";
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Code = "APP_R_ERR";
                response.ErrorMessage = ex.Message;
                response.Ex = ex;
                return response;
            }
        }

        private Request ChangeRequestStatus(int id, Status statusToChange)
        {
            Request requestToEdit = context.RequestRepository.GetById(id);

            Status originalStatus = requestToEdit.RequestStatus;

            requestToEdit.RequestDate = DateTime.Now;
            requestToEdit.RequestStatus = statusToChange;

            switch (statusToChange)
            {
                case Status.Approved:
                case Status.Recused:
                    if (originalStatus == Status.Pending)
                        context.RequestRepository.Update(requestToEdit);
                    break;
                case Status.Pending:
                    if (originalStatus == Status.Draft)
                        context.RequestRepository.Update(requestToEdit);
                    break;
                case Status.Cancelled:
                    if(originalStatus != Status.Cancelled && originalStatus != Status.Recused)
                        context.RequestRepository.Update(requestToEdit);
                    break;
                default:
                    break;
            }
            return requestToEdit;
        }
    }
}
