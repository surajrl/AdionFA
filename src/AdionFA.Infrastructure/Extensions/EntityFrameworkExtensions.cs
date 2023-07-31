using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Infrastructure.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T
            : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public static IQueryable<T> ThenIncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T
            : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.ThenIncludeMultiple(include));
            }

            return query;
        }
    }
}
