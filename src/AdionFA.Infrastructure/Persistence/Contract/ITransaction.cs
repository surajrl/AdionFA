using System;
namespace AdionFA.Infrastructure.Persistance.Contracts
{
    public interface ITransaction : IDisposable
    {
        ITransaction Transactional<T>();

        void ReleaseDbContext();
    }
}
