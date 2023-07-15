using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Mappers;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace AdionFA.Infrastructure.Helpers
{
    public static class CandleHelper
    {
        public static IEnumerable<Candle> GetHistoryCandles(string historyFilePath)
        {
            try
            {
                if (!File.Exists(historyFilePath))
                {
                    throw new Exception("File not found");
                }

                var result = new List<Candle>();

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                };

                using var reader = new StreamReader(historyFilePath);
                using var csv = new CsvReader(reader, config);

                csv.Context.RegisterClassMap<CandleMap>();
                var candles = csv.GetRecords<Candle>();

                foreach (var candle in candles)
                {
                    if (candle.Open < candle.Close)
                    {
                        candle.Label = "UP";
                    }

                    if (candle.Open > candle.Close)
                    {
                        candle.Label = "DOWN";
                    }

                    result.Add(candle);
                }

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}