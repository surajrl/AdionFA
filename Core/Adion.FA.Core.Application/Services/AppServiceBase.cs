using Adion.FA.Infrastructure.Common.Base;
using Adion.FA.Infrastructure.Common.Transaction.Contracts;
using Ninject;
using System;
using System.Runtime.CompilerServices;

namespace Adion.FA.Core.Application.Services
{
    public class AppServiceBase : InfrastructureServiceBase
    {
        #region Transactional

        [Inject]
        public ITransaction Transactional { get; set; }

        #endregion

        #region Ctor

        public AppServiceBase(
            [CallerMemberName] string memberName = "", 
            [CallerLineNumber] int lineNumber = 0, 
            [CallerFilePath] string sourceFilePath = "") : base(memberName, lineNumber, sourceFilePath)
        {
        }

        #endregion

        #region Data

        public IDisposable Transaction<T>() 
        {
            return Transactional.Transactional<T>();
        }

        public IDisposable Transaction<T, X>()
        {
            return Transactional.Transactional<T,X>();
        }

        public void Dispose() 
        {
            Transactional.ReleaseDbContext();
        }

        #endregion

        #region Logger

        public void LogInfoGet<T>(
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            base.LogInfoGet<T>(memberName: memberName, lineNumber: lineNumber, filePath: filePath);
        }

        public void LogInfoCreate<T>(
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            base.LogInfoCreate<T>(memberName: memberName, lineNumber: lineNumber, filePath: filePath);
        }

        public void LogInfoUpdate<T>(
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            base.LogInfoUpdate<T>(memberName: memberName, lineNumber: lineNumber, filePath: filePath);
        }

        public void LogException<T>(
            Exception ex,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        { 
            base.LogException<T>(ex, memberName: memberName, lineNumber: lineNumber, filePath: filePath);
        }

        #endregion
    }
}
