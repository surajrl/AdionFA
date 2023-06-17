using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AdionFA.Benchmark
{
    [MemoryDiagnoser]
    public class BacktestBenchmark
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<BacktestBenchmark>();
            //BacktestExecutionUsingIEnumerable();
        }

        private static List<int> _existingList = Enumerable.Repeat(2, 1000).ToList();

        [Benchmark]
        public static List<int> ListAddRange()
        {
            var newList = new List<int>();

            newList.Clear();
            newList.AddRange(_existingList);

            return newList;
        }

        [Benchmark]
        public static List<int> ListNew()
        {
            var newList = new List<int>(_existingList);
            return newList;
        }

        [Benchmark]
        public static List<int> ListForeachAdd()
        {
            var newList = new List<int>();

            newList.Clear();
            foreach (var item in _existingList)
            {
                newList.Add(item);
            }

            return newList;
        }

        //    private readonly static AdionFADbContext _db = new();

        //    public static void BacktestExecutionUsingIEnumerable()
        //    {
        //        Expression<Func<HistoricalData, bool>> predicate = hd => hd.HistoricalDataId == 1 && (hd.EndDate ?? DateTime.MinValue) == DateTime.MinValue;
        //        Expression<Func<HistoricalData, dynamic>> includes = hd => hd.HistoricalDataCandles;

        //        // Get candles from database
        //        var historicalData = FirstOrDefault(predicate, hd => hd.HistoricalDataCandles);
        //        var candles = historicalData.HistoricalDataCandles.
        //            Select(hdCandle => new Candle
        //            {
        //                Date = hdCandle.StartDate,
        //                Time = hdCandle.StartTime,
        //                Open = hdCandle.Open,
        //                High = hdCandle.High,
        //                Low = hdCandle.Low,
        //                Close = hdCandle.Close,
        //                Volume = hdCandle.Volume,
        //                Spread = hdCandle.Spread
        //            })
        //        .OrderBy(d => d.Date)
        //            .ThenBy(d => d.Time).ToList();

        //        var timeframeId = (int)TimeframeEnum.H4;
        //        var fromDate = new DateTime(2020, 01, 01);
        //        var toDate = new DateTime(2022, 01, 01);

        //        var node = StrategyBuilderService.DeserializeBacktest("C:\\Users\\suraj\\Desktop\\SBCorrelationUP\\BACKTEST-STOCHRSI.2_APO.2_STOCHRSI_CCI-1447.xml");

        //        var candlesRange = (from candle in candles
        //                            let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
        //                            where dt >= fromDate && dt <= toDate
        //                            select candle).ToList();

        //        var nodeIndicators = Extractor.BuildIndicatorsFromNode(node.Node);

        //        for (var candleIdx = 0; candleIdx < candlesRange.Count - 1; candleIdx++)
        //        {
        //            var firstCandle = candlesRange[0];
        //            var nextCandle = candlesRange[candleIdx + 1];
        //            var currentCandle = new Candle
        //            {
        //                Date = candlesRange[candleIdx].Date,
        //                Time = candlesRange[candleIdx].Time,

        //                Open = candlesRange[candleIdx].Open,
        //                High = candlesRange[candleIdx].Open,
        //                Low = candlesRange[candleIdx].Open,
        //                Close = candlesRange[candleIdx].Open,

        //                Spread = candlesRange[candleIdx].Spread
        //            };

        //            if (ApproveCandle(nodeIndicators, candleIdx, firstCandle, currentCandle, candlesRange))
        //            {
        //                var isWinnerTrade = false;
        //                var spread = currentCandle.Spread * 0.00001;

        //                isWinnerTrade = node.Label.ToLowerInvariant() == "up"
        //                    ? (currentCandle.Open + spread) < nextCandle.Open
        //                    : currentCandle.Open > (nextCandle.Open + spread);

        //                if (isWinnerTrade)
        //                {
        //                    Debug.WriteLine("Winning Trade!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("Losing Trade!");
        //                }
        //            }
        //        }

        //        return;
        //    }

        //    private static bool ApproveCandle(IList<IndicatorBase> nodeIndicators, int candleIdx, Candle firstCandle, Candle currentCandle, List<Candle> candlesRange)
        //    {
        //        var tempRemovedCandle = candlesRange[candleIdx];
        //        candlesRange.RemoveAt(candleIdx);
        //        candlesRange.Insert(candleIdx, currentCandle);

        //        var nodeIndicatorResults = Extractor.CalculateNodeIndicators(
        //            firstCandle,
        //            currentCandle,
        //            nodeIndicators,
        //            candlesRange.GetRange(0, candleIdx + 1));

        //        candlesRange.RemoveAt(candleIdx);
        //        candlesRange.Insert(candleIdx, tempRemovedCandle);


        //        for (var i = 0; i < nodeIndicatorResults.Count; i++)
        //        {
        //            var indicator = nodeIndicatorResults[i];

        //            if (indicator.OutNBElement == 0)
        //            {
        //                return false;
        //            }

        //            var output = indicator.Output[indicator.OutNBElement - 1];

        //            switch (indicator.Operator)
        //            {
        //                case MathOperatorEnum.GreaterThanOrEqual:
        //                    if (output >= indicator.Value)
        //                    {
        //                        break;
        //                    }
        //                    return false;

        //                case MathOperatorEnum.LessThanOrEqual:
        //                    if (output <= indicator.Value)
        //                    {
        //                        break;
        //                    }
        //                    return false;

        //                case MathOperatorEnum.GreaterThan:
        //                    if (output > indicator.Value)
        //                    {
        //                        break;
        //                    }
        //                    return false;

        //                case MathOperatorEnum.LessThan:
        //                    if (output < indicator.Value)
        //                    {
        //                        break;
        //                    }
        //                    return false;

        //                case MathOperatorEnum.Equal:
        //                    if (output == indicator.Value)
        //                    {
        //                        break;
        //                    }
        //                    return false;
        //            }
        //        }

        //        return true;
        //    }



        //    // Database Queries

        //    public static IQueryable<HistoricalData> GetAll()
        //    {
        //        var query = _db.Set<HistoricalData>().Where(e => e.IsDeleted == false);
        //        return query;
        //    }

        //    public static IQueryable<HistoricalData> GetAll(params string[] include)
        //    {
        //        var query = _db.Set<HistoricalData>().Where(e => e.IsDeleted == false);
        //        if (include != null && include.Any())
        //        {
        //            query = include.Aggregate(query, (current, includePath) => current.Include(includePath));
        //        }

        //        return query;
        //    }

        //    public static IQueryable<HistoricalData> GetAll(params Expression<Func<HistoricalData, dynamic>>[] includes)
        //    {
        //        var query = GetAll();

        //        if (includes != null)
        //        {
        //            query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
        //        }

        //        return query;
        //    }

        //    public static IQueryable<HistoricalData> GetAll(Expression<Func<HistoricalData, bool>> predicate)
        //    {
        //        var query = GetAll();
        //        if (predicate != null)
        //        {
        //            query = query.Where(predicate);
        //        }

        //        return query;
        //    }

        //    public static IQueryable<HistoricalData> GetAll(Expression<Func<HistoricalData, bool>> predicate, params Expression<Func<HistoricalData, dynamic>>[] includes)
        //    {
        //        var query = GetAll(predicate);

        //        if (includes != null)
        //        {
        //            query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
        //        }

        //        return query;
        //    }

        //    public static IQueryable<HistoricalData> GetAll(Expression<Func<HistoricalData, bool>> predicate, params string[] includes)
        //    {
        //        var query = GetAll(predicate);

        //        if (includes != null)
        //        {
        //            query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
        //        }

        //        return query;
        //    }

        //    // First Or Default

        //    public static HistoricalData FirstOrDefault()
        //    {
        //        var query = GetAll();
        //        return query.FirstOrDefault();
        //    }

        //    public static HistoricalData FirstOrDefault(Expression<Func<HistoricalData, bool>> predicate)
        //    {
        //        var query = GetAll(predicate);
        //        return query.FirstOrDefault();
        //    }

        //    public static HistoricalData FirstOrDefault(params Expression<Func<HistoricalData, dynamic>>[] includes)
        //    {
        //        var query = GetAll(includes);
        //        return query?.FirstOrDefault();
        //    }

        //    public static HistoricalData FirstOrDefault(Expression<Func<HistoricalData, bool>> predicate, params Expression<Func<HistoricalData, dynamic>>[] includes)
        //    {
        //        var query = GetAll(predicate, includes);
        //        return query.FirstOrDefault();
        //    }

        //    public static HistoricalData FirstOrDefault(Expression<Func<HistoricalData, bool>> predicate, params string[] includes)
        //    {
        //        var query = GetAll(predicate, includes);
        //        return query.FirstOrDefault();
        //    }
        //}
    }
}