using System.Collections.Generic;

namespace BestDoctors.DirectInsurance.Model.Generic.Geocoding
{
    public class CityGeoGridModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public int? ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int? CountryId { get; set; }
        public string CountryName { get; set; }

        public string Key { get; set; }
        public GeoSuggestionModel Geo { get; set; }
        public List<GeoSuggestionModel> GeoSuggestions { get; set; }
    }

    public class GeoSuggestionModel
    {
        public string Key => $"{Lat}_{Lng}";
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
    }
}