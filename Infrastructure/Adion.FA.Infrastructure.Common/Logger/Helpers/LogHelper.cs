﻿using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Common.Logger.Contracts;
using Adion.FA.Infrastructure.Common.Logger.Services;
using Adion.FA.Infrastructure.Common.Security.Helper;
using System;
using System.Runtime.CompilerServices;
using static Adion.FA.Infrastructure.Common.Logger.Enums.LoggerEnum;

namespace Adion.FA.Infrastructure.Common.Logger.Helpers
{
    public static class LogHelper
    {
        private static ILoggerHandler LoggerHandler = IoC.Get<ILoggerHandler>();

        public static void LogInfo<T>(LogModel model = null, LogActionEnum? action = null,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            LoggerHandler.LogInfo<T>(model ?? new LogModel
            {
                _tenantId = SecurityHelper.Identity.TenantId,
                _owner = SecurityHelper.Identity.Owner,
            }, action ?? LogActionEnum.Getting, memberName, lineNumber, filePath);
        }

        public static void LogException<T>(
            Exception ex,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            LoggerHandler.LogInfo<T>(new LogModel
            {
                _tenantId = SecurityHelper.Identity.TenantId,
                _owner = SecurityHelper.Identity.Owner,
                param0 = ex.ToString(),
            }, LogActionEnum.Exception, memberName, lineNumber, filePath);
        }
    }
}
