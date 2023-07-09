using AdionFA.Infrastructure.Common.Base;
using AdionFA.Infrastructure.Common.Transaction.Contracts;
using Ninject;
using System;
using System.Runtime.CompilerServices;

namespace AdionFA.Core.Application.Services
{
    public class AppServiceBase : InfrastructureServiceBase
    {
        [Inject]
        public ITransaction Transactional { get; set; }

        public AppServiceBase(
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string sourceFilePath = "")
            : base(memberName, lineNumber, sourceFilePath)
        {
        }

        // Data

        public IDisposable Transaction<T>()
        {
            return Transactional.Transactional<T>();
        }

        public IDisposable Transaction<T, X>()
        {
            return Transactional.Transactional<T, X>();
        }

        public void Dispose()
        {
            Transactional.ReleaseDbContext();
        }
    }
}
