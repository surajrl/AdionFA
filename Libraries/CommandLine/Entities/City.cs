using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestDoctors.DirectInsurance.Infrastructure.Contracts;

namespace BestDoctors.DirectInsurance.Core.Domain.Entities.Generic
{
    [Table(nameof(City), Schema = "Generic")]
    public class City : EntityBase, ITimeSensitive
    {
        [Key]
        public int CityId { get; set; }

        #region Properties
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ExternalId { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DPA_PPGA { get; set; }
        #endregion

        #region Navitation Properties
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        
        [ForeignKey(nameof(ProvinceId))]
        public Province Province { get; set; }

        public virtual ICollection<CityGeocoding> CityGeocodings { get; set; }
        #endregion
    }
}
