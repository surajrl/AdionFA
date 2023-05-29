using AdionFA.Infrastructure.Common.Attributes;
using AdionFA.Infrastructure.Common.Extensions;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Contracts;
using AdionFA.Infrastructure.Common.Logger.Extensions;
using AdionFA.Infrastructure.Common.Security.Helper;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static AdionFA.Infrastructure.Common.Logger.Enums.LoggerEnum;

namespace AdionFA.Infrastructure.Common.Logger.Services
{
    public class LogModelBase
    {
        [IgnoreReflection]
        public string Log { get; private set; }

        [IgnoreReflection]
        public LogActionModel ActionModel { get; private set; }

        // Audit 

        [IgnoreReflection]
        public string _owner { get; set; }

        private string LogAuditTemplate => "{0}.{1} call from --{2}-- line --{3}-- file --{4}";

        public void Build<T>(LogActionEnum action,
            string memberName, int lineNumber, string filePath)
        {
            ActionModel = action.GetModel();
            PropertyInfo[] props = GetType().GetFilteredProperties<IgnoreReflectionAttribute>();

            List<string> values = props.Where(_p => !string.IsNullOrEmpty(_p.GetValue(this) as string))
                .Select(_p => _p.GetValue(this) as string).ToList();

            int count = Regex.Matches(ActionModel.Template, @"(?<!\{)\{([0-9]+).*?\}(?!})")  //select all placeholders - placeholder ID as separate group
                 .Cast<Match>() // cast MatchCollection to IEnumerable<Match>, so we can use Linq
                 .Max(m => int.Parse(m.Groups[1].Value)) + 1; // select maximum value of first group (it's a placegolder ID) converted to int

            for (int i = 0; i < count; i++)
            {
                if (values.ElementAtOrDefault(i) == null)
                {
                    values.Insert(i, "n/a");
                }
            }

            _owner = _owner ?? SecurityHelper.Identity.Owner;

            Log = string.Format(ActionModel.Template, values.ToArray());

            if (ActionModel.Type == LogTypeEnum.Audit)
            {
                values.Insert(0, typeof(T).Name);
                Log = string.Format(LogAuditTemplate, _owner, string.Format(ActionModel.Template, values.ToArray()),
                    memberName, lineNumber, filePath);
            }
        }
    }

    public class LogModel : LogModelBase
    {
        public string param0 { get; set; }
        public string param1 { get; set; }
    }

    public class LoggerHandler : ILoggerHandler
    {
        public void LogInfo<T>(LogModelBase model, LogActionEnum action,
            [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            model._owner = model._owner ?? "Anonymous";

            model.Build<T>(action, memberName, lineNumber, filePath);
            LogInfo(model);
        }

        private void LogInfo(LogModelBase model)
        {
            Log.Logger = IoC.Get<ILogger>();
            switch (model.ActionModel.Level)
            {
                case LogLevelEnum.Info:
                    Log.Information(model.Log);
                    break;
                case LogLevelEnum.Warning:
                    Log.Warning(model.Log);
                    break;
                case LogLevelEnum.Error:
                    Log.Error(model.Log);
                    break;
                case LogLevelEnum.Fatal:
                    Log.Fatal(model.Log);
                    break;
            }
        }
    }
}
