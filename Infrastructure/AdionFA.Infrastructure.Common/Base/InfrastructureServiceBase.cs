using AdionFA.Infrastructure.Common.Comparators;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Contracts;
using AdionFA.Infrastructure.Common.Logger.Services;
using AdionFA.Infrastructure.Common.Security.Authorization;
using AdionFA.Infrastructure.Common.Security.Helper;
using AdionFA.Infrastructure.Common.Security.Model;
using AdionFA.TransferObject.Base;
using AutoMapper;
using Ninject;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using static AdionFA.Infrastructure.Common.Logger.Enums.LoggerEnum;

namespace AdionFA.Infrastructure.Common.Base
{
    public interface IServiceBase
    { }

    public abstract class InfrastructureServiceBase : IServiceBase
    {
        public readonly string _tenantId;
        public readonly string _ownerId;
        public readonly string _owner;

        // Infrastructure

        private AdionIdentity Identity => SecurityHelper.Identity;

        public IComparator Comparator => IoC.Get<IComparator>();
        public IMapper Mapper => IoC.Get<IMapper>();
        public IKernel Kernel => IoC.Get<IKernel>();
        public ILoggerHandler Logger => IoC.Get<ILoggerHandler>();

        public InfrastructureServiceBase(
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string sourceFilePath = "")
        {
            try
            {
                _tenantId = Identity.TenantId;
                _ownerId = Identity.OwnerId;
                _owner = Identity.Owner;

                Type type = AuthorizationMgmt.AuthorizeCall(_tenantId, _ownerId, _owner, sourceFilePath);

                // Log Info
                MethodInfo m = typeof(InfrastructureServiceBase).GetMethod(nameof(InfrastructureServiceBase.LogInfoGet));
                m.MakeGenericMethod(type).Invoke(this, new object[] { new LogModel
                {
                    _tenantId = _tenantId,
                    _owner = _owner
                }, memberName, lineNumber, sourceFilePath });
            }
            catch (Exception ex)
            {
                LogException<Exception>(ex, new LogModel
                {
                    _tenantId = _tenantId,
                    _owner = _owner
                }, memberName, lineNumber, sourceFilePath);

                throw;
            }
        }

        // Logger

        public void LogInfoGet<T>(LogModel model = null,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            Logger.LogInfo<T>(model ?? new LogModel
            {
                _tenantId = _tenantId,
                _owner = _owner,
            }, LogActionEnum.Getting, memberName, lineNumber, filePath);
        }

        public void LogInfoCreate<T>(LogModel model = null,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            Logger.LogInfo<T>(model ?? new LogModel
            {
                _tenantId = _tenantId,
                _owner = _owner,
            }, LogActionEnum.Creating, memberName, lineNumber, filePath);
        }

        public void LogInfoUpdate<T>(LogModel model = null,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            Logger.LogInfo<T>(model ?? new LogModel
            {
                _tenantId = _tenantId,
                _owner = _owner,
            }, LogActionEnum.Updating, memberName, lineNumber, filePath);
        }

        public void LogException<T>(
            Exception ex, LogModel model = null,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            Logger.LogInfo<T>(model ?? new LogModel
            {
                _tenantId = _tenantId,
                _owner = _owner,
                param0 = ex.ToString(),
            }, LogActionEnum.Exception, memberName, lineNumber, filePath);
        }
    }
}
