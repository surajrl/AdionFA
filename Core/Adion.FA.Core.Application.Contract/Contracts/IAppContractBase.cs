using System;

namespace Adion.FA.Core.Application.Contract.Contracts
{
    public interface IAppContractBase : IDisposable
    {
        public IDisposable Transaction<T>();
        public IDisposable Transaction<T, X>();
    }
}
