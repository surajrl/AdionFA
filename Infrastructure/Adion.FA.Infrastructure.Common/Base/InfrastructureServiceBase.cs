using Adion.FA.Infrastructure.Common.Comparators;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Common.Logger.Contracts;
using Adion.FA.Infrastructure.Common.Logger.Services;
using Adion.FA.Infrastructure.Common.Security.Authorization;
using Adion.FA.Infrastructure.Common.Security.Helper;
using Adion.FA.Infrastructure.Common.Security.Model;
using Adion.FA.TransferObject.Base;
using AutoMapper;
using Ninject;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using static Adion.FA.Infrastructure.Common.Logger.Enums.LoggerEnum;

namespace Adion.FA.Infrastructure.Common.Base
{
    public interface IServiceBase
    { }

    public abstract class InfrastructureServiceBase : IServiceBase
    {
        public readonly string _tenantId;
        public readonly string _ownerId;
        public readonly string _owner;

        #region Infrastructure

        private AdionIdentity Identity => SecurityHelper.Identity;

        public IComparator Comparator => IoC.Get<IComparator>();

        public IMapper Mapper => IoC.Get<IMapper>();

        public IKernel Kernel => IoC.Get<IKernel>();

        public ILoggerHandler Logger => IoC.Get<ILoggerHandler>();

        #endregion

        #region Ctor

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
                
                //LogInfo
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

        #endregion

        #region Logger

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

        #endregion
    }
}
