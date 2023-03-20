using Adion.FA.Infrastructure.Enums;
using System;

namespace Adion.FA.Infrastructure.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime BuildDateTime(int periodId, DateTime dt, long time, bool isTicks = false)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0);
            if (isTicks)
            {
                return dt.AddTicks(time);
            }
            else
            {
                switch (periodId)
                {
                    case (int)CurrencyPeriodEnum.M1:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)CurrencyPeriodEnum.M5:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)CurrencyPeriodEnum.M15:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)CurrencyPeriodEnum.M30:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)CurrencyPeriodEnum.H1:
                        ts = new TimeSpan(0, (int)time, 0, 0);
                        break;
                    case (int)CurrencyPeriodEnum.H4:
                        ts = new TimeSpan(0, (int)time, 0, 0);
                        break;
                    case (int)CurrencyPeriodEnum.Daily:
                        ts = new TimeSpan((int)time, 0, 0, 0);
                        break;
                    case (int)CurrencyPeriodEnum.W:
                        ts = new TimeSpan((int)time, 0, 0, 0);
                        break;
                    case (int)CurrencyPeriodEnum.MN:
                        ts = new TimeSpan((int)time, 0, 0, 0);
                        break;
                }
                return dt.Add(ts);
            }
        }
    }
}
