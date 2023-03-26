using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Contracts.Bases;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Core.Domain.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdionFA.Infrastructure.Core.Data.Repositories.Extension
{
    public static class RepositoryExtension
    {
        #region CloseTemporalRecord

        public static TEntity CloseTemporalRecord<TEntity>(this IRepository<TEntity> repository) where TEntity : EntityBase, ITimeSensitive
        {
            var old = repository.LastTemporalRecord();
            if (old != null)
            {
                repository.Update((TEntity)old.GetClosedTamporalRecord());
            }
        
            return old;
        }

        #endregion


        public static bool IsActive<TEntity>(this TEntity entity, DateTime? asOfDate = null) where TEntity : EntityBase, ITimeSensitive
        {
            DateTime dt = asOfDate ?? DateTime.UtcNow;

            return 
                (entity.StartDate.Date <= dt.Date &&
                ((entity.EndDate ?? DateTime.MinValue) == DateTime.MinValue || entity.EndDate.Value.Date >= dt.Date)) ||
                entity.StartDate.Date > dt.Date &&
                ((entity.EndDate ?? DateTime.MinValue) == DateTime.MinValue || entity.StartDate.Date <= entity.EndDate.Value.Date);
        }
    }
}
