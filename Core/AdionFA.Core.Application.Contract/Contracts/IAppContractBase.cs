using System;

namespace AdionFA.Core.Application.Contract.Contracts
{
    public interface IAppContractBase : IDisposable
    {
        public IDisposable Transaction<T>();
        public IDisposable Transaction<T, X>();
    }
}
