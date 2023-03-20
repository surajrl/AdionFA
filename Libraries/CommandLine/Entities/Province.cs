using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestDoctors.DirectInsurance.Core.Domain.Entities.Generic
{
    [Table(nameof(Province), Schema = "Generic")]
    public class Province : EntityBase
    {
        [Key]
        public int ProvinceId { get; set; }

        #region Constructors
        public Province()
        {
            Cities = new List<City>();
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int? ExternalId { get; set; }
        public string ISO { get; set; }
        public string DPA_PPGA { get; set; }
        public int? IssuerId { get; set; }
        #endregion


        #region Navigation Properties
        public ICollection<City> Cities { get; set; }
        
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        #endregion
    }
}
