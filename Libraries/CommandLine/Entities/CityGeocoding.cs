using BestDoctors.DirectInsurance.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestDoctors.DirectInsurance.Core.Domain.Entities.Generic
{
    [Table(nameof(CityGeocoding), Schema = "Generic")]
    public class CityGeocoding : EntityBase, ITimeSensitive
    {
        [Key]
        public int CityGeocodingId { get; set; }

        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }

        public string Address { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
