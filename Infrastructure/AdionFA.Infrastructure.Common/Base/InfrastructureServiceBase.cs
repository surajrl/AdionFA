using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Security.Authorization;
using AdionFA.Infrastructure.Common.Security.Helper;
using AdionFA.Infrastructure.Common.Security.Model;
using AutoMapper;
using Ninject;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdionFA.Infrastructure.Common.Base
{
    public interface IServiceBase
    {
    }

    public abstract class InfrastructureServiceBase : IServiceBase
    {
        private readonly string _ownerId;
        private readonly string _owner;

        // Infrastructure

        private AdionIdentity Identity => SecurityHelper.Identity;

        public IMapper Mapper => IoC.Get<IMapper>();
        public IKernel Kernel => IoC.Get<IKernel>();

        public InfrastructureServiceBase(
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string sourceFilePath = "")
        {
            try
            {
                _ownerId = Identity.OwnerId;
                _owner = Identity.Owner;

                var type = AuthorizationMgmt.AuthorizeCall(_ownerId, _owner, sourceFilePath);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
