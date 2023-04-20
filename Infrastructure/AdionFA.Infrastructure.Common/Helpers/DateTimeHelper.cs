using AdionFA.Infrastructure.Enums;
using System;

namespace AdionFA.Infrastructure.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime BuildDateTime(int periodId, DateTime dt, long time, bool isTicks = false)
        {
            var ts = new TimeSpan(0, 0, 0, 0);
            if (isTicks)
            {
                return dt.AddTicks(time);
            }
            else
            {
                switch (periodId)
                {
                    case (int)TimeframeEnum.M1:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)TimeframeEnum.M5:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)TimeframeEnum.M15:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)TimeframeEnum.M30:
                        ts = new TimeSpan(0, 0, (int)time, 0);
                        break;
                    case (int)TimeframeEnum.H1:
                        ts = new TimeSpan(0, (int)time, 0, 0);
                        break;
                    case (int)TimeframeEnum.H4:
                        ts = new TimeSpan(0, (int)time, 0, 0);
                        break;
                    case (int)TimeframeEnum.D1:
                        ts = new TimeSpan((int)time, 0, 0, 0);
                        break;
                    case (int)TimeframeEnum.W1:
                        ts = new TimeSpan((int)time, 0, 0, 0);
                        break;
                    case (int)TimeframeEnum.MN1:
                        ts = new TimeSpan((int)time, 0, 0, 0);
                        break;
                }
                return dt.Add(ts);
            }
        }
    }
}
