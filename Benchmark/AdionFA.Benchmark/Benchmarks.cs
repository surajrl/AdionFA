using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Core.Data.Persistence;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Data;

namespace AdionFA.Benchmark
{
    public class Benchmarks
    {
        private AdionFADbContext _dbContext = new();

        private Consumer _consumer = new();

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }

        [Benchmark]
        public async void First()
        {
            var hd = _dbContext.HistoricalDatas.FirstOrDefault(id => id.HistoricalDataId == 1);

            IEnumerable<Candle>? c = hd?.HistoricalDataCandles.Select(
                h => new Candle
                {
                    Date = h.StartDate,
                    Time = h.StartTime,
                    Open = h.Open,
                    High = h.High,
                    Low = h.Low,
                    Close = h.Close,
                    Volume = h.Volume,
                    Label = h.Close > h.Open ? "UP" : "DOWN"
                })
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time).ToList();

            _consumer.Consume<IEnumerable<Candle>>(c);
        }
    }
}
