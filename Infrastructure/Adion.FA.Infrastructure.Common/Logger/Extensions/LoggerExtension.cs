using Adion.FA.Infrastructure.Common.Logger.Attributes;
using static Adion.FA.Infrastructure.Common.Logger.Enums.LoggerEnum;
using System;

namespace Adion.FA.Infrastructure.Common.Logger.Extensions
{
    public class LogActionModel 
    {
        public LogLevelEnum Level { get; set; }
        public LogTypeEnum Type { get; set; }
        public string Template { get; set; }
    }

    public static class LoggerExtension
    {
        public static LogActionModel GetModel(this LogActionEnum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                System.Reflection.FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    LogActionAttribute attr = (LogActionAttribute)Attribute.GetCustomAttribute(field, typeof(LogActionAttribute));
                    if (attr != null)
                    {
                        return new LogActionModel
                        {
                            Level = attr.Level,
                            Type = attr.Type,
                            Template = attr.Template
                        };
                    }
                }
            }

            return null;
        }
    }
}
