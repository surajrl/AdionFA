using AdionFA.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AdionFA.Domain.Extensions
{
    public static class EntityExtension
    {
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