using AdionFA.Infrastructure.Common.Logger.Services;
using System.Runtime.CompilerServices;
using static AdionFA.Infrastructure.Common.Logger.Enums.LoggerEnum;

namespace AdionFA.Infrastructure.Common.Logger.Contracts
{
    public interface ILoggerHandler
    {
        void LogInfo<T>(LogModelBase model, LogActionEnum action,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "");
    }
}
