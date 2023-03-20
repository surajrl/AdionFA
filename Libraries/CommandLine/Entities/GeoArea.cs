using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestDoctors.DirectInsurance.Core.Domain.Entities.Generic
{
    [Table(nameof(GeoArea), Schema = "Generic")]
    public class GeoArea : EntityBase
    {
        [Key]
        public int GeoAreaId { get; set; }

        #region Constructors
        public GeoArea()
        {
            Countries = new List<Country>();
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        #endregion

        #region Navigator Properties
        public ICollection<Country> Countries { get; set; }
        #endregion
    }
}
