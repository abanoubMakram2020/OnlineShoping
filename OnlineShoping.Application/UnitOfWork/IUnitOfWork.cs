using System;
using System.Threading.Tasks;

namespace OnlineShoping.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task StartTransactionScope();
        Task TransactionScopeComplete();
        Task TransactionScopeDispose();
        Task<int> Complete();
    }
}
