using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestDoctors.DirectInsurance.Core.Domain.Entities.Generic
{
    [Table(nameof(Country), Schema = "Generic")]
    public class Country : EntityBase
    {
        [Key]
        public int CountryId { get; set; }

        #region Constructors
        public Country()
        {
            Cities = new List<City>();
            Provinces = new List<Province>();
        }
        #endregion

        #region Properties
        public string Code { get; set; }
        public string Name { get; set; }
        public int? GeoAreaId { get; set; }
        public string A3Code { get; set; }
        public bool BDCommercial { get; set; }

        public bool ValidateCodeOfConduct { get; set; }

        public bool ShowBDAcademy { get; set; }

        public int? SaCode { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<City> Cities { get; set; }
        public ICollection<Province> Provinces { get; set; }
        
        [ForeignKey(nameof(GeoAreaId))]
        public GeoArea GeoArea { get; set; }
        #endregion
    }
}
