using Adion.FA.Infrastructure.Common.Transaction.Contracts;
using Adion.FA.Infrastructure.Core.Data.Persistence;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Adion.FA.Infrastructure.Core.Data.Persistence.EFCore;
using System;

namespace Adion.FA.Infrastructure.Common.Transaction.Services
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork<AdionSecurityDbContext> _adionSecurityUOfW;
        private readonly IUnitOfWork<AdionFADbContext> _adionFAUOfW;

        private bool _isCommit = true;

        #region Ctor
        public Transaction(
            IUnitOfWork<AdionSecurityDbContext> adionSecurityUOfW,
            IUnitOfWork<AdionFADbContext> adionFAUOfW)
        {
            _adionSecurityUOfW = adionSecurityUOfW;
            _adionFAUOfW = adionFAUOfW;

            AppDomain.CurrentDomain.FirstChanceException += FirstChanceException_Handler;
        }
        #endregion

        private void FirstChanceException_Handler(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException -= FirstChanceException_Handler;
            _isCommit = false;
        }

        public ITransaction Transactional<T>()
        {
            if (typeof(T).FullName == typeof(IAdionFADbContext).FullName)
                _adionFAUOfW.BeginTransaction();

            if (typeof(T).FullName == typeof(IAdionSecurityDbContext).FullName)
                _adionSecurityUOfW.BeginTransaction();

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

            if (_adionSecurityUOfW.HasTransactionActive())
                _adionSecurityUOfW.Commit();
        }

        public void Rollback()
        {
            if (_adionFAUOfW.HasTransactionActive())
                _adionFAUOfW.Rollback();

            if (_adionSecurityUOfW.HasTransactionActive())
                _adionSecurityUOfW.Rollback();
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
            _adionSecurityUOfW.Dispose();
        }

        public void ReleaseDbContext()
        {
            if (!_adionFAUOfW.HasTransactionActive())
                _adionFAUOfW.Dispose();

            if (!_adionSecurityUOfW.HasTransactionActive())
                _adionSecurityUOfW.Dispose();
        }
    }
}
