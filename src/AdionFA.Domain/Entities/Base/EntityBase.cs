using AdionFA.Domain.Contracts.Bases;
using System;

namespace AdionFA.Domain.Entities.Base
{
    public class EntityBase : IAuditBase
    {
        public bool IsDeleted { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }

        public string UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

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