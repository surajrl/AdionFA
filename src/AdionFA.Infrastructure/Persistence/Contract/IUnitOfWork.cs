using Microsoft.EntityFrameworkCore;
using System;

namespace AdionFA.Infrastructure.Persistance.Contracts
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IDisposable BeginTransaction();
        bool HasTransactionActive();
        void SaveChanges();
        void Commit();
        void Rollback();
    }
}
