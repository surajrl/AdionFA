using Adion.FA.Infrastructure.Common.Logger.Services;
using System.Runtime.CompilerServices;
using static Adion.FA.Infrastructure.Common.Logger.Enums.LoggerEnum;

namespace Adion.FA.Infrastructure.Common.Logger.Contracts
{
    public interface ILoggerHandler
    {
        void LogInfo<T>(LogModelBase model, LogActionEnum action,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "");
    }
}
