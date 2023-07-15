using AdionFA.Infrastructure.Persistance.Contracts;
using System;

namespace AdionFA.Infrastructure.Persistence.EFCore
{
    public class Transactions : ITransaction
    {
        private readonly IUnitOfWork<AdionFADbContext> _adionFAUOfW;
        private bool _isCommit = true;
        private bool _disposedValue;

        public Transactions(IUnitOfWork<AdionFADbContext> adionFAUOfW)
        {
            _adionFAUOfW = adionFAUOfW;

            AppDomain.CurrentDomain.FirstChanceException += FirstChanceException_Handler;
        }

        private void FirstChanceException_Handler(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException -= FirstChanceException_Handler;
            _isCommit = false;
        }

        public ITransaction Transactional<T>()
        {
            if (typeof(T).FullName == typeof(IAdionFADbContext).FullName)
            {
                _adionFAUOfW.BeginTransaction();
            }

            return this;
        }

        public void ReleaseDbContext()
        {
            if (!_adionFAUOfW.HasTransactionActive())
                _adionFAUOfW.Dispose();
        }

        // IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_isCommit)
                    {
                        if (_adionFAUOfW.HasTransactionActive())
                        {
                            _adionFAUOfW.Commit();
                        }
                    }
                    else
                    {
                        if (_adionFAUOfW.HasTransactionActive())
                        {
                            _adionFAUOfW.Rollback();
                        }
                    }

                    _adionFAUOfW.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
