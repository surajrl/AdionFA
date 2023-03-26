using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Mappers;
using AdionFA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Helpers
{
    public static class CandleHelper
    {
        /// <summary>
        /// Reads a CSV file containing historical data and returns it
        /// as a list of candles (date, time, open, high, low, close) assigning
        /// a label to indicate if the price went up or down in that period
        /// </summary>
        /// <param name="historyFilePath"></param>
        /// <returns></returns>
        public static IEnumerable<Candle> GetHistoryCandles(string historyFilePath)
        {
            try
            {
                if (!File.Exists(historyFilePath))
                    throw new Exception(ExceptionMessagesEnum.FileNotFound.GetMetadata().Description);

                List<Candle> result = new();

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                };

                using var reader = new StreamReader(historyFilePath);
                using var csv = new CsvReader(reader, config);
                
                csv.Context.RegisterClassMap<CandleMap>();
                var candles = csv.GetRecords<Candle>();

                foreach (var r in candles)
                {
                    if (r.Open < r.Close)
                    {
                        r.Label = "UP";
                    }
                    if (r.Open > r.Close)
                    {
                        r.Label = "DOWN";
                    }

                    if (r.Open != r.Close)
                    {
                        result.Add(r);
                    }
                }

                return result;
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
                DataTable dt = new();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                };

                using var reader = new StreamReader(historyFilePath);
                using var csv = new CsvReader(reader, config);
                
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
                    dt.Rows.Add(new object[] { r.Date.Date.Date, r.Time, r.Open, r.High, r.Low, r.Close, r.Volume });
                }

                return dt;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
