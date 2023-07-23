using System;

namespace AdionFA.Domain.Entities.Base
{
    public abstract class EntityBase
    {

        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public object Id()
        {
            var prop = GetType().GetProperty(string.Concat(GetType().Name, nameof(Id)));
            if (prop != null)
            {
                return prop.GetValue(this);
            }
            return null;
        }
    }
}