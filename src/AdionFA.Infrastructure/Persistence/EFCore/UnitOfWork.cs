using AdionFA.Infrastructure.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdionFA.Infrastructure.Persistence.EFCore
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        // Identity

        public string OwnerId { get; }
        public string Owner { get; }

        public TContext Context { get; }

        private bool _disposed = false;

        public UnitOfWork(string ownerId, string owner, TContext context)
        {
            Context = context;
            Context.Database.SetCommandTimeout(120);

            OwnerId = ownerId;
            Owner = owner;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public IDisposable BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public bool HasTransactionActive()
        {
            return !_disposed && Context.Database.CurrentTransaction != null;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Commit()
        {
            if (!_disposed)
            {
                Context.Database.CurrentTransaction?.Commit();
            }
        }

        public void Rollback()
        {
            if (!_disposed)
            {
                Context.Database.CurrentTransaction?.Rollback();
            }
        }
    }
}
