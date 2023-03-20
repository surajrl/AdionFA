using Adion.FA.Core.Domain.Contracts.Bases;
using System;
using System.Linq;
using System.Reflection;

namespace Adion.FA.Core.Domain.Aggregates.Base
{
    public class EntityBase : IAuditBase
    {
        #region Audit
        
        public bool IsDeleted { get; set; }

        public bool Inaccesible { get; set; }

        public string TenantId { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }

        public string UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        #endregion

        public object Id()
        {
            PropertyInfo prop = GetType().GetProperty(string.Concat(GetType().Name, nameof(Id)));
            if (prop != null)
            {
                return prop.GetValue(this);
            }
            return null;
        }

        public EntityBase ShallowCopy(bool cleanEntityRef = false)
        {
            var entity = (EntityBase)this.MemberwiseClone();

            if (cleanEntityRef)
            {
                var properties = GetType().GetProperties().Where(p => p.CanWrite).ToList();

                foreach (var info in properties)
                {
                    if (info.PropertyType.IsSubclassOf(typeof(EntityBase)))
                    {
                        info.SetValue(entity, null);
                    }
                    else if (info.PropertyType.GetGenericArguments().Any(type => type.IsSubclassOf(typeof(EntityBase))))
                    {
                        var z = info.GetValue(entity);
                        var tz = (z != null) ? Activator.CreateInstance(z.GetType()) : null;
                        info.SetValue(entity, tz);
                    }
                }
            }

            return entity;
        }
    }
}
