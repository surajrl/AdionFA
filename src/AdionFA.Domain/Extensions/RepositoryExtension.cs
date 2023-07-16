using AdionFA.Domain.Contracts.Bases;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities.Base;
using System;

namespace AdionFA.Domain.Extensions
{
    public static class RepositoryExtension
    {
        public static TEntity CloseTemporalRecord<TEntity>(this IGenericRepository<TEntity> repository) where TEntity : EntityBase, ITimeSensitive
        {
            var old = repository.LastTemporalRecord();
            if (old != null)
            {
                repository.Update((TEntity)old.GetClosedTamporalRecord());
            }

            return old;
        }

        public static bool IsActive<TEntity>(this TEntity entity, DateTime? asOfDate = null) where TEntity : EntityBase, ITimeSensitive
        {
            var dt = asOfDate ?? DateTime.UtcNow;

            return
                (entity.StartDate.Date <= dt.Date &&
                ((entity.EndDate ?? DateTime.MinValue) == DateTime.MinValue || entity.EndDate.Value.Date >= dt.Date)) ||
                entity.StartDate.Date > dt.Date &&
                ((entity.EndDate ?? DateTime.MinValue) == DateTime.MinValue || entity.StartDate.Date <= entity.EndDate.Value.Date);
        }
    }
}
