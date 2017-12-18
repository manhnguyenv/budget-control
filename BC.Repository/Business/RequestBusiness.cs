using BC.Repository.Context;
using BC.Repository.Domain;
using System;
using System.Collections.Generic;

namespace BC.Repository.Business
{
    public class RequestBusiness
    {
        private IUnitOfWork<Request> context;

        public RequestBusiness(IUnitOfWork<Request> unitOfWork)
        {
            context = unitOfWork;
        }

        public IEnumerable<Request> GetRequestsByIdProject(int idProject)
        {
            return context.Where(r => r.IdProject.Equals(idProject), n => n.Project, n => n.Supplier);
        }

        public Request GetRequestById(int? id)
        {
            if (id == null) return null;
            return context.GetById(id);
        }

        public int CreateRequest(Request requestToCreate)
        {
            requestToCreate.RequestStatus = Status.Draft;
            requestToCreate.RequestDate = DateTime.Now;
            return context.Save(requestToCreate);
        }

        public int EditRequest(Request dataToEdit)
        {
            Request requestToEdit = context.GetById(dataToEdit.Id);

            if (requestToEdit.RequestStatus == Status.Draft)
            {
                requestToEdit.IdProject = dataToEdit.IdProject;
                requestToEdit.IdSupplier = dataToEdit.IdSupplier;
                requestToEdit.Value = dataToEdit.Value;

                return context.Update(requestToEdit);
            }
            return 0;
        }

        public int ChangeRequestStatus(int id, Status statusToChange)
        {
            Request requestToEdit = context.GetById(id);

            Status originalStatus = requestToEdit.RequestStatus;

            requestToEdit.RequestDate = DateTime.Now;
            requestToEdit.RequestStatus = statusToChange;

            switch (statusToChange)
            {
                case Status.Approved:
                case Status.Recused:
                    return (originalStatus == Status.Pending) ? context.Update(requestToEdit) : 0;
                case Status.Pending:
                    return (originalStatus == Status.Draft) ? context.Update(requestToEdit) : 0;
                case Status.Cancelled:
                    return context.Update(requestToEdit);
                default:
                    return 0;
            }
        }
    }
}
