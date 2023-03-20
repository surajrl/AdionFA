using System;
namespace Adion.FA.Infrastructure.Common.Transaction.Contracts
{
    public interface ITransaction : IDisposable
    {
        ITransaction Transactional<T>();
        ITransaction Transactional<T, X>();

        void ReleaseDbContext();
    }
}
