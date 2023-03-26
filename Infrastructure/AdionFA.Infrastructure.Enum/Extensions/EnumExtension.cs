using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.Enums.Attributes;
using System;
using System.ComponentModel;

namespace AdionFA.Infrastructure.Enums
{
    public static class EnumExtension
    {
        public static Metadata GetMetadata(this Enum value) 
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                System.Reflection.FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    MetadataAttribute attr = (MetadataAttribute)Attribute.GetCustomAttribute(field, typeof(MetadataAttribute));
                    if (attr != null)
                    {
                        return new Metadata(attr.Code, attr.Name, attr.Description, attr.Symbol, attr.Value, (int)field.GetValue(value));
                    }
                }
            }
            return null;
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                System.Reflection.FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return string.Empty;
        }

        public static long ToSeconds(this TimeframeEnum value) 
        {
            long MinuteOnSeconds = 60;
            long HourOnSecond = MinuteOnSeconds * 60;
            long DayOnSecond = HourOnSecond * 24; 
            long time = 0;
            switch (value)
            {
                case TimeframeEnum.MN1:
                    time = DayOnSecond * 30;
                    break;
                case TimeframeEnum.W1:
                    time = DayOnSecond * 7;
                    break;
                case TimeframeEnum.D1:
                    time = 24 * HourOnSecond;
                    break;
                case TimeframeEnum.H4:
                    time = 4 * HourOnSecond;
                    break;
                case TimeframeEnum.H1:
                    time = HourOnSecond;
                    break;
                case TimeframeEnum.M30:
                    time = 30 * MinuteOnSeconds;
                    break;
                case TimeframeEnum.M15:
                    time = 15 * MinuteOnSeconds;
                    break;
                case TimeframeEnum.M5:
                    time = 5 * MinuteOnSeconds;
                    break;
                case TimeframeEnum.M1:
                    time = MinuteOnSeconds;
                    break;
            }

            return time;
        }
    }
}
