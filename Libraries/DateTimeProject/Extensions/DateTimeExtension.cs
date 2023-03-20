using DateTimeProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DateTimeProject.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime Add(this DateTime date, int value, int? uOfM = (int)UoMEnum.Year)
        {
            switch (uOfM)
            {
                case (int)UoMEnum.Month:
                    return date.AddMonths(value);

                case (int)UoMEnum.Day:
                    return date.AddDays(value);

                default:
                    return date.AddYears(value);
            }
        }

        public static int DiffTimeInUoM(this DateTime from, DateTime? to = null, int? uOfM = (int)UoMEnum.Year)
        {
            to = to ?? DateTime.UtcNow;

            int sign = from > to ? -1 : 1;
            DateTime fromTemp = from < to ? from : to.Value;
            DateTime toTemp = from < to ? to.Value : from;

            TimeSpan ts = toTemp - fromTemp;

            int diff = 0;
            if (fromTemp < toTemp)
            {
                switch (uOfM)
                {
                    case (int)UoMEnum.Month:
                        diff = ((fromTemp.Year - toTemp.Year) * 12) + fromTemp.Month - toTemp.Month; 
                        break;
                    case (int)UoMEnum.Week:
                        diff = (int)(ts.TotalDays/4);
                        break;
                    case (int)UoMEnum.Day:
                        diff = (int)ts.TotalDays;
                        break;
                    case (int)UoMEnum.Hour:
                        diff = (int)ts.TotalHours;
                        break;
                    case (int)UoMEnum.Minute:
                        diff = (int)ts.TotalMinutes;
                        break;
                    case (int)UoMEnum.Second:
                        diff = (int)ts.TotalSeconds;
                        break;

                    default:
                        diff = (new DateTime(1, 1, 1) + ts).Year - 1;
                        break;
                }
            }
            return diff * sign;
        }

        public static int GetAge(this DateTime birthdate)
        {
            return GetAge(birthdate, DateTime.Today);
        }

        public static int GetAge(this DateTime birthdate, DateTime relativeDate)
        {
            // Calculate the age.
            var age = relativeDate.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate.Date > relativeDate.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        /// Convert From UTC to Local based on TimeZone info
        /// </summary>
        /// <param name="utcDateTime"></param>
        /// <param name="timeZoneInfoId"></param>
        /// <returns></returns>
        public static DateTime ToLocal(this DateTime utcDateTime, string timeZoneInfoId)
        {
            return string.IsNullOrEmpty(timeZoneInfoId)
                ? utcDateTime
                : TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoId));
        }

        public static IEnumerable<DateTime> GetRange(this DateTime fromDate, DateTime toDate)
        {
            if (fromDate.Date > toDate.Date)
                throw new Exception("fromDate must be less than toDate");

            var range = new List<DateTime>();
            var from = fromDate.Date;
            var to = toDate.Date;
            while (from <= to)
            {
                range.Add(from);
                from = from.AddDays(1);
            }
            return range;
        }

        public static IEnumerable<DateTime> GetRangeForValuationDates(this DateTime fromDate, DateTime toDate, bool includeToDate = false, int? explicitDay = null)
        {
            var range = GetRange(fromDate, toDate);
            var dates = explicitDay is null
                ? range.Where(i => i.Day == 15 || i.Day == 28).ToList()
                : range.Where(i => i.Day == explicitDay.Value).ToList();

            if (includeToDate && dates.All(i => i.Date != toDate.Date))
                dates.Add(toDate.Date);

            return dates;
        }

        public static bool IsValidPastDate(this DateTime? date, bool allowNull = false, bool includeCurrentDate = true)
        {
            if (allowNull && date is null)
                return true;

            if (!allowNull && date is null)
                return false;

            return IsValidPastDate(date.Value, includeCurrentDate);
        }

        public static bool IsValidPastDate(this DateTime date, bool includeCurrentDate = true)
        {
            if (date.Year < 1900)
                return false;

            if (!includeCurrentDate && date.Date >= DateTime.UtcNow.Date)
                return false;

            if (includeCurrentDate && date.Date > DateTime.UtcNow.Date)
                return false;

            return true;
        }
    }
}
