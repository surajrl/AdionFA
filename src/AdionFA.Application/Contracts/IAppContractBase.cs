using System;

namespace AdionFA.Application.Contracts
{
    public interface IAppContractBase : IDisposable
    {
        public IDisposable Transaction<T>();
    }
}
