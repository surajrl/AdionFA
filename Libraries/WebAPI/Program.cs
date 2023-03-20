using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI
{
    class Program
    {
        public CountryModel _country;
        public ProvinceModel _province;
        public List<CityModel> _cities;

        static void Main(string[] args)
        {
            var p = new Program();
            p._country = new CountryModel { Id = 1, Name = "Cuba" };
            p._province = new ProvinceModel { Id = 1, Name = "Las Tunas" };
            p._cities = new List<CityModel> 
            {
                new CityModel{ Id = 1, Name = "Puerto Padre" },
                new CityModel{ Id = 2, Name = "Jesús Menéndez" },
                new CityModel{ Id = 2, Name = "Amancio" },
                new CityModel{ Id = 2, Name = "Colombia" },
                new CityModel{ Id = 2, Name = "Manatí" }
            };

            var result = p.GeocodingRequest();
        }

        public List<CityGeoGridModel> GeocodingRequest()
        {
            var result = new List<CityGeoGridModel>();

            string url = $"{ConfigurationManager.AppSettings["positionstackApi"]}";
            string countryParam = _country != null ? "," + _country.Name : string.Empty;
            string provinceParam = _province != null ? "," + _province.Name : string.Empty;

            var meta = _cities.Select(c => new
            {
                city = new { id = c.Id, name = c.Name },
                url = $"{url}&query={c.Name}{provinceParam}{countryParam}"
            }).ToArray();
            
            Parallel.ForEach(meta, new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1,
            }, m =>
            {
                string apiresponse = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(m.url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            GeoDataResponseModel geo = null;
                            apiresponse = reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(apiresponse))
                            {
                                geo = JsonConvert.DeserializeObject<GeoDataResponseModel>(apiresponse);
                            }

                            var firstGeo = geo.Data.FirstOrDefault();
                            result.Add(new CityGeoGridModel
                            {
                                CityId = m.city.id,
                                CityName = m.city.name,

                                ProvinceId = _province?.Id,
                                ProvinceName = _province?.Name,

                                CountryId = _country?.Id,
                                CountryName = _country?.Name,

                                Lat = firstGeo?.Latitude,
                                Lng = firstGeo?.Longitude,

                                GeoSuggestions = geo?.Data?.Select(s => new GeoSuggestionModel
                                {
                                    Lat = s.Latitude,
                                    Lng = s.Longitude,
                                    Address = $"{s.Label}"
                                }).ToList(),
                            });
                        }
                    }
                }
            });

            return result;
        }

        
        public async Task<List<CityGeoGridModel>> GeocodingRequest2()
        {
            string url = ConfigurationManager.AppSettings["positionstackApi"];
            if (!string.IsNullOrEmpty(url))
            {
                string countryParam = _country != null ? "," + _country.Name : string.Empty;
                string provinceParam = _province != null ? "," + _province.Name : string.Empty;
                var urls = _cities.Select(c => new
                {
                    cityId = c.Id,
                    url = $"{url}&query={c.Name}{provinceParam}{countryParam}"
                }).ToArray();

                if (urls.Length > 0)
                {
                    using (var client = new HttpClient())
                    {
                        var request = urls.Select(u => new
                        {
                            cityId = u.cityId,
                            response = client.GetAsync(u.url),
                        });
                        await Task.WhenAll(request.Select(r => r.response));

                        //Get the responses
                        var responses = request.Select
                            (
                                task => task.response.Result
                            );

                        foreach (var r in responses)
                        {
                            // Extract the message body
                            var s = await r.Content.ReadAsStringAsync();
                            Console.WriteLine(s);
                        }

                        return _cities.Select(_c => new CityGeoGridModel
                        {
                            CityId = _c.Id,
                            CityName = _c.Name,

                            /*GeoSuggestions = (from r in request
                                              let geo = JsonConvert.DeserializeObject<GeoDataModel>(r.response.Result.Content.ReadAsStringAsync().Result)
                                             where r.cityId == _c.Id
                                             select new GeoSuggestionModel
                                             {
                                                Lat = geo.Latitude
                                             }).ToList()*/
                        }).ToList();
                    }
                }
            }

            return Array.Empty<CityGeoGridModel>().ToList();
        }
        
        public static GeoDataResponseModel HttpWebRequest(string url)
        {
            string apiresponse = null;
            //string query = $"query={"Puerto Padre"}{",Las Tunas"}{",Cuba"}";
            //string url = $"{ConfigurationManager.AppSettings["positionstackApi"]}&{query}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 15 * 60 * 1000;
            request.KeepAlive = true;
            request.ReadWriteTimeout = 15 * 60 * 1000;
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        apiresponse = reader.ReadToEnd();
                    }
                }
            }

            return JsonConvert.DeserializeObject<GeoDataResponseModel>(apiresponse);
        }
    }
}
