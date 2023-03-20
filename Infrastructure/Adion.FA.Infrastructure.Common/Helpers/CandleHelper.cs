using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Mappers;
using Adion.FA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Adion.FA.Infrastructure.Common.Helpers
{
    public static class CandleHelper
    {
        public static IEnumerable<Candle> GetHistoryCandles(string historyFilePath)
        {
            try
            {
                if (!File.Exists(historyFilePath))
                    throw new Exception(ExceptionMessagesEnum.FileNotFound.GetMetadata().Description);

                List<Candle> result = new List<Candle>();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                };
                using (var reader = new StreamReader(historyFilePath))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<CandleMap>();

                    var candles = csv.GetRecords<Candle>();

                    foreach (var r in candles)
                    {
                        if (r.open < r.close)
                        {
                            r.label = "UP";
                        }
                        if (r.open > r.close)
                        {
                            r.label = "DOWN";
                        }

                        if (r.open != r.close)
                        {
                            result.Add(r);
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public static DataTable GetHistoryCandlesDT(string historyFilePath)
        {
            try
            {
                DataTable dt = new DataTable();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                };
                using (var reader = new StreamReader(historyFilePath))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<CandleMap>();

                    var candles = csv.GetRecords<Candle>();

                    dt.Columns.Add("date", typeof(string));
                    dt.Columns.Add("time", typeof(string));
                    dt.Columns.Add("open", typeof(string));
                    dt.Columns.Add("max", typeof(string));
                    dt.Columns.Add("min", typeof(string));
                    dt.Columns.Add("close", typeof(string));
                    dt.Columns.Add("volume", typeof(string));
                    foreach (var r in candles)
                    {
                        dt.Rows.Add(new object[] { r.date.Date.Date, r.time, r.open, r.max, r.min, r.close, r.volumen });
                    }

                    return dt;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
