using BestDoctors.DirectInsurance.Core.Domain.Entities.Generic;
using CommandLine.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommandLine.Persistence
{
    public class GeocodingDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=PREDATOR-PC\LOCALHOST;Database=Geocoding;Trusted_Connection=True");

#if DEBUG
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(b => b.AddDebug()));
#endif
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<GeoArea> GeoAreas { get; set; }
        public DbSet<CityGeocoding> CityGeocodings { get; set; }
        public DbSet<MasterData> MasterDatas { get; set; }
    }
}
