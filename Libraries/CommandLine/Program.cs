using BestDoctors.DirectInsurance.Core.Domain.Entities.Generic;
using BestDoctors.DirectInsurance.Model.Generic.Geocoding;
using BestDoctors.DirectInsurance.Model.Generic.Geocoding.PositionstackProvider;
using CommandLine.Entities;
using CommandLine.Persistence;
using CsvHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CommandLine
{
    class Program
    {
        private static void Main(string[] args)
        {
            //PopulateMasterData();
            //Geocoding();

            using var dbcontext = new GeocodingDBContext();

            #region Plan
            DataTable dt = new DataTable();
            DbCommand cmd = null; 
            using (cmd = dbcontext.Database.GetDbConnection().CreateCommand())
            {
                bool isOpen = cmd.Connection.State == ConnectionState.Open;
                if (!isOpen)
                {
                    cmd.Connection.Open();
                }

                cmd.CommandText = "SET SHOWPLAN_ALL ON";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select * from Generic.City";
                cmd.CommandType = CommandType.Text;
                using (var dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);
                }
                cmd.CommandText = "SET SHOWPLAN_ALL OFF";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                //cmd.Connection.Close();
            }

            Console.WriteLine(cmd?.Connection?.State == ConnectionState.Open);
            Console.WriteLine(dbcontext.Database.GetDbConnection().State == ConnectionState.Open);


            DataColumnCollection columns = dt.Columns;
            foreach (DataRow row in dt.Rows)
            {
                var TotalSubtreeCost = dt.Columns.Contains("TotalSubtreeCost") ? row["TotalSubtreeCost"].ToString() : string.Empty;
                Console.WriteLine(TotalSubtreeCost);
            }
            #endregion

            //var cities = (from c in dbcontext.Cities
            //             where c.Lat != null && c.Lng != null
            //             select new
            //             {
            //                 c.CityId,
            //                 c.Lat,
            //                 c.Lng
            //             }).ToList();


            //Console.WriteLine(cities.Count);

            //using var sw = File.CreateText("geocoding.txt");
            //cities.ForEach(c => sw.WriteLine($"({c.CityId},{c.Lat},{c.Lng})"));
        }


        public static void Geocoding()
        {
            #region Geocoding
            using var dbcontext = new GeocodingDBContext();
            IEnumerable<int> master = (from md in dbcontext.MasterDatas
                                       where md.City != "Unknown" && md.Province != "Unknown"
                                             && md.CityId != null && md.ProvinceId != null
                                             && md.City != null && md.Province != null
                                       select md.CityId.Value).ToList();

            IEnumerable<int> processedCityIds = (from cg in dbcontext.CityGeocodings
                                                 group cg by cg.CityId into cityId
                                                 select cityId.Key).ToList();

            IEnumerable<City> cities = (from c in dbcontext.Cities
                                        where !processedCityIds.Contains(c.CityId) && master.Contains(c.CityId)
                                        select c)
                                       .Include(c => c.Province).ThenInclude(p => p.Country)
                                       .Include(c => c.Country).ToList();

            var provinces = (from c in cities
                             group c by new
                             {
                                 Prov = c.Province,
                                 Country = c.Country
                             } into prov
                             select prov).Take(100).ToList();

            foreach (var prov in provinces)
            {
                var provCities = prov.ToList();
                List<CityGeoGridModel> cityGeoGridModels = GeoProvider1(prov.Key.Country, prov.Key.Prov, provCities);
                Console.WriteLine($"Country: {prov.Key.Country.Name}  Province: {prov.Key.Prov.Name}  Total Cities processed: {cityGeoGridModels.Count}");
                if (cityGeoGridModels.Any())
                {
                    foreach (var cgg in cityGeoGridModels)
                    {
                        City c = dbcontext.Cities.FirstOrDefault(_c => _c.CityId == cgg.CityId);
                        c.Lat = cgg.Geo.Lat;
                        c.Lng = cgg.Geo.Lng;

                        c.CityGeocodings = (from sugg in cgg.GeoSuggestions
                                            select new CityGeocoding
                                            {
                                                CityId = cgg.CityId,
                                                Lat = sugg.Lat,
                                                Lng = sugg.Lng,
                                                Address = sugg.Address
                                            }).ToList();

                        dbcontext.Update(c);
                        dbcontext.SaveChanges();
                    }
                }
            }
            #endregion
        }


        public static List<CityGeoGridModel> GeoProvider1(Country _country, Province _province, List<City> cities)
        {
            var result = new List<CityGeoGridModel>();

            string url = $"{ConfigurationManager.AppSettings["geocodingApi"]}";
            string countryParam = BuildParamUrl(null, _country?.Name);
            string provinceParam = BuildParamUrl(null, _province?.Name);

            var meta = cities.Where(
                c => (_province == null || (_province.ProvinceId > 0 && c.ProvinceId == _province.ProvinceId)) &&
                     (_country == null || (_country.CountryId > 0 && c.CountryId == _country.CountryId))
            ).Select(c => new
            {
                city = new { id = c.CityId, name = c.Name, province = c.Province?.Name },
                url = $"{url}&query={c.Name}{(!string.IsNullOrEmpty(provinceParam) ? provinceParam : BuildParamUrl(null, c.Province?.Name))}{countryParam}"
            }).ToArray();

            meta = (meta.Count() > 500 ? meta.Take(500) : meta).ToArray();

            Parallel.ForEach(meta, new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1,
            }, m =>
            {
                try
                {
                    string apiresponse = null;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(m.url);
                    request.KeepAlive = true;
                    request.Timeout = 1 * 60 * 1000;
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
                                    try
                                    {
                                        geo = JsonConvert.DeserializeObject<GeoDataResponseModel>(apiresponse);
                                    }
                                    catch (Exception ex)
                                    {
                                        geo = new GeoDataResponseModel();
                                    }
                                }

                                List<GeoSuggestionModel> geoSuggestions = geo?.Data?.Select(s => new GeoSuggestionModel
                                {
                                    Lat = s.Latitude,
                                    Lng = s.Longitude,
                                    Address = BuildAddressLabel(s),
                                })?.ToList() ?? Array.Empty<GeoSuggestionModel>().ToList();

                                if (geoSuggestions.Any())
                                {
                                    GeoSuggestionModel firstGeo = geoSuggestions.FirstOrDefault();
                                    var cgg = new CityGeoGridModel
                                    {
                                        CityId = m.city.id,
                                        CityName = m.city.name,

                                        ProvinceId = _province?.ProvinceId,
                                        ProvinceName = _province?.Name ?? m.city.province,

                                        CountryId = _country?.CountryId,
                                        CountryName = _country?.Name,

                                        Key = firstGeo.Key,
                                        Geo = firstGeo,
                                        GeoSuggestions = geoSuggestions,
                                    };
                                    result.Add(cgg);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            });

            return result;


            string BuildAddressLabel(GeoDataModel model)
            {
                if (model != null)
                {
                    string delimiter = ", ";
                    string name = !string.IsNullOrEmpty(model.Name) ? delimiter + model.Name : string.Empty;
                    string postalCode = !string.IsNullOrEmpty(model.PostalCode) ? delimiter + model.PostalCode : string.Empty;
                    string region = !string.IsNullOrEmpty(model.Region) ? delimiter + model.Region : string.Empty;
                    string country = !string.IsNullOrEmpty(model.Country) ? delimiter + model.Country : string.Empty;

                    string address = $"{name}{postalCode}{region}{country}".TrimStart(delimiter.ToArray());
                    return address;
                }
                return string.Empty;
            }

            string BuildParamUrl(string key, string param)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return !string.IsNullOrEmpty(param) ? $"&{key}={param}" : string.Empty;
                }
                else
                {
                    return !string.IsNullOrEmpty(param) ? $",{param}" : string.Empty;
                }
            }
        }

        public static void PopulateMasterData()
        {
            using var dbcontext = new GeocodingDBContext();
            using var tran = dbcontext.Database.BeginTransaction();
            {
                try
                {
                    #region Master Data
                    using (var reader = new StreamReader("MasterData.csv"))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        using (var dr = new CsvDataReader(csv))
                        {
                            var dt = new DataTable();
                            dt.Load(dr);

                            if (dt.Rows.Count > 0)
                            {
                                var list = new List<MasterData>();
                                foreach (var row in dt.AsEnumerable())
                                {
                                    var array = row.ItemArray;

                                    var md = new MasterData();
                                    md.PolicyNumber = array[0].ToString();

                                    if (int.TryParse(array[1].ToString(), out int policyId))
                                        md.PolicyId = policyId;

                                    if (int.TryParse(array[2].ToString(), out int issuerId))
                                        md.IssuerId = issuerId;

                                    md.Code = array[3].ToString();
                                    md.PolicyStatusName = array[4].ToString();

                                    if (int.TryParse(array[5].ToString(), out int memberId))
                                        md.MemberId = memberId;

                                    if (int.TryParse(array[6].ToString(), out int contactId))
                                        md.ContactId = contactId;

                                    if (int.TryParse(array[7].ToString(), out int MemberTypeId))
                                        md.MemberTypeId = MemberTypeId;

                                    md.FirstName = array[8].ToString();
                                    md.LastName = array[9].ToString();

                                    if (int.TryParse(array[10].ToString(), out int ContactTypeId))
                                        md.ContactTypeId = ContactTypeId;

                                    md.ContactType = array[11].ToString();

                                    if (int.TryParse(array[12].ToString(), out int LineOfBusinessId))
                                        md.LineOfBusinessId = LineOfBusinessId;

                                    if (int.TryParse(array[13].ToString(), out int CityId))
                                        md.CityId = CityId;

                                    md.Street = array[14].ToString();

                                    if (int.TryParse(array[15].ToString(), out int ProvinceId))
                                        md.ProvinceId = ProvinceId;

                                    if (int.TryParse(array[16].ToString(), out int CountryId))
                                        md.CountryId = CountryId;

                                    md.AddresType = array[17].ToString();
                                    md.Country = array[18].ToString();
                                    md.CountryName = array[19].ToString();
                                    md.Province = array[20].ToString();
                                    md.City = array[21].ToString();
                                    md.Pais = array[22].ToString();

                                    list.Add(md);
                                }

                                dbcontext.AddRange(list);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    tran.Rollback();
                }
            }
        }
    }
}
