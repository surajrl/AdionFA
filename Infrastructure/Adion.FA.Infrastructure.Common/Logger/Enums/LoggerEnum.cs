using Adion.FA.Infrastructure.Common.Logger.Attributes;

namespace Adion.FA.Infrastructure.Common.Logger.Enums
{
    public static class LoggerEnum
    {
        public enum LogTypeEnum
        {
            Audit = 1,
            Bussines = 2
        }

        public enum LogLevelEnum
        {
            Info = 1,
            Warning = 2,
            Error = 3,
            Fatal = 4,
        }

        public enum LogActionEnum
        {
            #region Common

            [LogAction(LogLevelEnum.Info, LogTypeEnum.Audit, "Getting --{0}--")]
            Getting,

            [LogAction(LogLevelEnum.Info, LogTypeEnum.Audit, "Getting API --{0}--")]
            GettingAPI,

            [LogAction(LogLevelEnum.Info, LogTypeEnum.Audit, "Creating --{0}--")]
            Creating,

            [LogAction(LogLevelEnum.Info, LogTypeEnum.Audit, "Updating --{0}--")]
            Updating,

            [LogAction(LogLevelEnum.Error, LogTypeEnum.Audit, "Exception from --{0}-- Details --{1}--")]
            Exception,

            #endregion
        }
    }
}
