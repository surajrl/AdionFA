using System;

namespace BestDoctors.DirectInsurance.Core.Domain.Entities
{
    public abstract class EntityBase
    {
        #region Fields
        private object _tag;
        #endregion

        #region Constructors
        protected EntityBase()
        {
        }
        #endregion

        #region Properties
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
