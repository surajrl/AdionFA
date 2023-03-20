using Newtonsoft.Json;
using System.Collections.Generic;

namespace BestDoctors.DirectInsurance.Model.Generic.Geocoding.PositionstackProvider
{
    public class GeoDataResponseModel
    {
        public GeoDataResponseModel()
        {
            Data = new List<GeoDataModel>();
        }

        [JsonConstructor]
        public GeoDataResponseModel(
            [JsonProperty("data")] List<GeoDataModel> data
        )
        {
            this.Data = data;
        }

        [JsonProperty("data")]
        public readonly List<GeoDataModel> Data;
    }
}
