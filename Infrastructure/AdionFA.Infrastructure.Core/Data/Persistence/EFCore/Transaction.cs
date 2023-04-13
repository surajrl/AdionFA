using AdionFA.Infrastructure.Common.Transaction.Contracts;
using AdionFA.Infrastructure.Core.Data.Persistence;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using AdionFA.Infrastructure.Core.Data.Persistence.EFCore;
using System;

namespace AdionFA.Infrastructure.Common.Transaction.Services
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork<AdionFADbContext> _adionFAUOfW;

        private bool _isCommit = true;

        #region Constructor

        public Transaction(IUnitOfWork<AdionFADbContext> adionFAUOfW)
        {
            _adionFAUOfW = adionFAUOfW;

            AppDomain.CurrentDomain.FirstChanceException += FirstChanceException_Handler;
        }

        #endregion Constructor

        private void FirstChanceException_Handler(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException -= FirstChanceException_Handler;
            _isCommit = false;
        }

        public ITransaction Transactional<T>()
        {
            if (typeof(T).FullName == typeof(IAdionFADbContext).FullName)
                _adionFAUOfW.BeginTransaction();

            return this;
        }

        public ITransaction Transactional<T, X>()
        {
            Transactional<T>();
            Transactional<X>();

            return this;
        }

        public void Commit()
        {
            if (_adionFAUOfW.HasTransactionActive())
                _adionFAUOfW.Commit();
        }

        public void Rollback()
        {
            if (_adionFAUOfW.HasTransactionActive())
                _adionFAUOfW.Rollback();
        }

        public void Dispose()
        {
            if (_isCommit)
            {
                Commit();
            }
            else
            {
                Rollback();
            }

            _adionFAUOfW.Dispose();
        }

        public void ReleaseDbContext()
        {
            if (!_adionFAUOfW.HasTransactionActive())
                _adionFAUOfW.Dispose();
        }
    }
}
