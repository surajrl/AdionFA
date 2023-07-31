using AdionFA.Domain.Enums.Market;
using System;

namespace AdionFA.Infrastructure.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime BuildDateTime(int timeframeId, DateTime date, long time)
        {
            var timeSpan = new TimeSpan(0, 0, 0, 0);

            switch (timeframeId)
            {
                case (int)TimeframeEnum.M1:
                    timeSpan = new TimeSpan(0, 0, (int)time, 0);
                    break;

                case (int)TimeframeEnum.M5:
                    timeSpan = new TimeSpan(0, 0, (int)time, 0);
                    break;

                case (int)TimeframeEnum.M15:
                    timeSpan = new TimeSpan(0, 0, (int)time, 0);
                    break;

                case (int)TimeframeEnum.M30:
                    timeSpan = new TimeSpan(0, 0, (int)time, 0);
                    break;

                case (int)TimeframeEnum.H1:
                    timeSpan = new TimeSpan(0, (int)time, 0, 0);
                    break;

                case (int)TimeframeEnum.H4:
                    timeSpan = new TimeSpan(0, (int)time, 0, 0);
                    break;

                case (int)TimeframeEnum.D1:
                    timeSpan = new TimeSpan((int)time, 0, 0, 0);
                    break;

                case (int)TimeframeEnum.W1:
                    timeSpan = new TimeSpan((int)time, 0, 0, 0);
                    break;

                case (int)TimeframeEnum.MN1:
                    timeSpan = new TimeSpan((int)time, 0, 0, 0);
                    break;
            }

            return date.Add(timeSpan);
        }
    }
}
