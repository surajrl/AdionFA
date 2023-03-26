using AdionFA.Core.Domain.Contracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Core.Domain.Extensions
{
    public static class TimeSensitiveExtensions
    {
        public static bool IsActive(this ITimeSensitive model, DateTime? today = null)
        {
            today = today ?? DateTime.UtcNow;
            return model != null && (model.StartDate.Date <= today.Value.Date) &&
                    (model.EndDate.Value.Date >= today.Value.Date || !model.EndDate.HasValue);
        }

        public static bool IsActiveInFuture(this ITimeSensitive model, DateTime? today = null)
        {
            today = today ?? DateTime.UtcNow;
            return model != null && model.StartDate.Date > today.Value.Date &&
                    (model.EndDate.Value.Date >= model.StartDate.Date || !model.EndDate.HasValue);
        }

        public static IQueryable<T> AllActive<T>(this IEnumerable<T> query) where T : class, ITimeSensitive
        {
            IQueryable<T> query1 = query.AsQueryable().IsActive(DateTime.UtcNow);

            return query1;
        }

        public static IQueryable<T> IsActive<T>(this IQueryable<T> query, DateTime? today = null) where T : class, ITimeSensitive
        {
            today = today ?? DateTime.UtcNow;
            query = query.Where(p => p.IsActive(today));

            return query;
        }

        public static IQueryable<T> IsActiveInFuture<T>(this IQueryable<T> query, DateTime? today = null) where T : class, ITimeSensitive
        {
            today = today ?? DateTime.UtcNow;
            Expression<Func<T, bool>> inner = p => true;
            query = query.Where(p => p.IsActiveInFuture(today));

            return query;
        }

        public static IQueryable<T> IsActiveOrActiveInFuture<T>(this IQueryable<T> query, DateTime? today = null) where T : class, ITimeSensitive
        {
            today = today ?? DateTime.UtcNow;
            Expression<Func<T, bool>> inner = p => true;
            query =
                query.Where(
                    p => (p.StartDate <= today && (!p.EndDate.HasValue || p.EndDate.Value >= today))
                          ||
                          (p.StartDate > today && !p.EndDate.HasValue));

            return query;
        }

        public static bool IsValid(this ITimeSensitive record)
        {
            if (record == null)
                return false;

            return (record.EndDate.HasValue && record.StartDate.Date <= record.EndDate.Value.Date)
                   ||
                   (!record.EndDate.HasValue);
        }

        public static bool Between(this DateTime date, DateTime startDate, DateTime? endDate)
        {
            return (startDate.Date <= date.Date) && (endDate == null || endDate.Value.Date >= date.Date);
        }
    }
}
