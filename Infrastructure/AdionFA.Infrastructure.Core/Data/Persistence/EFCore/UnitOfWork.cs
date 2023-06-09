﻿using Microsoft.EntityFrameworkCore;
using System;

namespace AdionFA.Infrastructure.Core.Data.Persistence
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        // Identity

        public string _ownerId { get; }
        public string _owner { get; }

        public TContext _context { get; }

        private bool _disposed = false;

        public UnitOfWork(string ownerId, string owner, TContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(120);

            _ownerId = ownerId;
            _owner = owner;
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
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public IDisposable BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public bool HasTransactionActive()
        {
            return !_disposed && _context.Database.CurrentTransaction != null;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Commit()
        {
            if (!_disposed)
                _context.Database.CurrentTransaction?.Commit();
        }

        public void Rollback()
        {
            if (!_disposed)
                _context.Database.CurrentTransaction?.Rollback();
        }
    }
}
