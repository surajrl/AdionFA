using System.Collections.Generic;

namespace WebAPI.Model
{
    public class CityGeoGridModel
    {
        public int? CityId { get; set; }
        public string CityName { get; set; }

        public int? ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int? CountryId { get; set; }
        public string CountryName { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public List<GeoSuggestionModel> GeoSuggestions { get; set; }
    }

    public class GeoSuggestionModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
    }
}
