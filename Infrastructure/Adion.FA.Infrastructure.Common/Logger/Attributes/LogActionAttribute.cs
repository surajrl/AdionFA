using static Adion.FA.Infrastructure.Common.Logger.Enums.LoggerEnum;
using System;

namespace Adion.FA.Infrastructure.Common.Logger.Attributes
{
    public class LogActionAttribute : Attribute
    {
        private LogLevelEnum _level;
        private LogTypeEnum _type;
        private string _template;

        public LogActionAttribute(
            LogLevelEnum level, LogTypeEnum type, string template)
        {
            _type = type;
            _level = level;
            _template = template;
        }

        public LogLevelEnum Level 
        { 
            get => _level; 
            set => _level = value; 
        }

        public LogTypeEnum Type
        {
            get => _type;
            set => _type = value;
        }

        public string Template 
        {
            get => _template; 
            set => _template = value;
        }
    }
}
