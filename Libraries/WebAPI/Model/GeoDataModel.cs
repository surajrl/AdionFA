using Newtonsoft.Json;

namespace WebAPI.Model
{
    public class GeoDataModel
    {
        [JsonConstructor]
        public GeoDataModel(
            [JsonProperty("latitude")] double latitude,
            [JsonProperty("longitude")] double longitude,
            [JsonProperty("type")] string type,
            [JsonProperty("name")] string name,
            [JsonProperty("number")] object number,
            [JsonProperty("postal_code")] object postalCode,
            [JsonProperty("street")] object street,
            [JsonProperty("confidence")] int confidence,
            [JsonProperty("region")] string region,
            [JsonProperty("region_code")] string regionCode,
            [JsonProperty("county")] string county,
            [JsonProperty("locality")] string locality,
            [JsonProperty("administrative_area")] object administrativeArea,
            [JsonProperty("neighbourhood")] object neighbourhood,
            [JsonProperty("country")] string country,
            [JsonProperty("country_code")] string countryCode,
            [JsonProperty("continent")] string continent,
            [JsonProperty("label")] string label
        )
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Type = type;
            this.Name = name;
            this.Number = number;
            this.PostalCode = postalCode;
            this.Street = street;
            this.Confidence = confidence;
            this.Region = region;
            this.RegionCode = regionCode;
            this.County = county;
            this.Locality = locality;
            this.AdministrativeArea = administrativeArea;
            this.Neighbourhood = neighbourhood;
            this.Country = country;
            this.CountryCode = countryCode;
            this.Continent = continent;
            this.Label = label;
        }

        [JsonProperty("latitude")]
        public readonly double Latitude;

        [JsonProperty("longitude")]
        public readonly double Longitude;

        [JsonProperty("type")]
        public readonly string Type;

        [JsonProperty("name")]
        public readonly string Name;

        [JsonProperty("number")]
        public readonly object Number;

        [JsonProperty("postal_code")]
        public readonly object PostalCode;

        [JsonProperty("street")]
        public readonly object Street;

        [JsonProperty("confidence")]
        public readonly int Confidence;

        [JsonProperty("region")]
        public readonly string Region;

        [JsonProperty("region_code")]
        public readonly string RegionCode;

        [JsonProperty("county")]
        public readonly string County;

        [JsonProperty("locality")]
        public readonly string Locality;

        [JsonProperty("administrative_area")]
        public readonly object AdministrativeArea;

        [JsonProperty("neighbourhood")]
        public readonly object Neighbourhood;

        [JsonProperty("country")]
        public readonly string Country;

        [JsonProperty("country_code")]
        public readonly string CountryCode;

        [JsonProperty("continent")]
        public readonly string Continent;

        [JsonProperty("label")]
        public readonly string Label;
    }
}
