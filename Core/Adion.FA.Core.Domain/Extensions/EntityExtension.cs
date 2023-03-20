using Adion.FA.Core.Domain.Aggregates.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Adion.FA.Core.Domain.Extensions
{
    public static class EntityExtension
    {
        public static TEntity CloneEntity<TEntity>(this TEntity entity, bool cleanNavRef = true) where TEntity : EntityBase
        {
            var clone = (TEntity)entity.ShallowCopy(cleanNavRef);

            if (clone == null) throw new Exception($"Clone Error for Entity: {entity.GetType().Name}");

            return clone;
        }

        public static int GetId<TEntity>(this TEntity entity) where TEntity : EntityBase
        {
            var attrType = typeof(KeyAttribute);
            var property = entity.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.GetCustomAttributes(attrType, false).Any());
            var value = (int)property.GetValue(entity);

            return value;
        }
    }
}
