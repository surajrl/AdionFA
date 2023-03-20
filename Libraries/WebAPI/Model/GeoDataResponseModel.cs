using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPI.Model
{
    public class GeoDataResponseModel
    {
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
