using System;
namespace AdionFA.Infrastructure.Common.Transaction.Contracts
{
    public interface ITransaction : IDisposable
    {
        ITransaction Transactional<T>();
        ITransaction Transactional<T, X>();

        void ReleaseDbContext();
    }
}
