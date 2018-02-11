using BC.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BC.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Budget> BudgetRepository { get; }
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Project> ProjectRepository { get; }
        IRepository<Request> RequestRepository { get; }
        IRepository<RequestHistoric> RequestHistoricRepository { get; }
        IRepository<Supplier> SupplierRepository { get; }
        void Commit();
    }
}