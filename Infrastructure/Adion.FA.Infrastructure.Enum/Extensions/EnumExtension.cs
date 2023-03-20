using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.Infrastructure.Enums.Attributes;
using System;
using System.ComponentModel;

namespace Adion.FA.Infrastructure.Enums
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

        public static long ToSeconds(this CurrencyPeriodEnum value) 
        {
            long MinuteOnSeconds = 60;
            long HourOnSecond = MinuteOnSeconds * 60;
            long DayOnSecond = HourOnSecond * 24; 
            long time = 0;
            switch (value)
            {
                case CurrencyPeriodEnum.MN:
                    time = DayOnSecond * 30;
                    break;
                case CurrencyPeriodEnum.W:
                    time = DayOnSecond * 7;
                    break;
                case CurrencyPeriodEnum.Daily:
                    time = 24 * HourOnSecond;
                    break;
                case CurrencyPeriodEnum.H4:
                    time = 4 * HourOnSecond;
                    break;
                case CurrencyPeriodEnum.H1:
                    time = HourOnSecond;
                    break;
                case CurrencyPeriodEnum.M30:
                    time = 30 * MinuteOnSeconds;
                    break;
                case CurrencyPeriodEnum.M15:
                    time = 15 * MinuteOnSeconds;
                    break;
                case CurrencyPeriodEnum.M5:
                    time = 5 * MinuteOnSeconds;
                    break;
                case CurrencyPeriodEnum.M1:
                    time = MinuteOnSeconds;
                    break;
            }

            return time;
        }
    }
}
