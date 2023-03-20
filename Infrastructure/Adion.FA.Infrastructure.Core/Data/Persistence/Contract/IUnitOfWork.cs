using Adion.FA.Core.Domain.Contracts.Bases;
using Microsoft.EntityFrameworkCore;
using System;

namespace Adion.FA.Infrastructure.Core.Data.Persistence
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
