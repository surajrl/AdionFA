using AdionFA.Core.Domain.Contracts.Bases;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdionFA.Infrastructure.Core.Data.Persistence
{
    public interface IUnitOfWork<TContext> : IIdentityBase, IDisposable where TContext : DbContext
    {
        IDisposable BeginTransaction();
        bool HasTransactionActive();
        void SaveChanges();
        void Commit();
        void Rollback();
    }
}
