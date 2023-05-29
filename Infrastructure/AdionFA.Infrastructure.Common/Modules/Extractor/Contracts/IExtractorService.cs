using AdionFA.Infrastructure.Common.Extractor.Model;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Extractor.Contracts
{
    public interface IExtractorService
    {
        IList<IndicatorBase> BuildIndicatorsFromCSV(string path);

        IList<IndicatorBase> BuildIndicatorsFromNode(IList<string> node);

        IList<IndicatorBase> DoExtraction(
            DateTime from,
            DateTime to,
            IList<IndicatorBase> indicators,
            IList<Candle> candles,
            int timeframeId,
            decimal variation = 0);

        IList<IndicatorBase> CalculateNodeIndicators(
            Candle firstCandle,
            Candle currentCandle,
            IList<IndicatorBase> indicators,
            IEnumerable<Candle> candleHistory);

        IList<Candle> GetCandles(string historyFilePath);

        bool ExtractorWrite(string path, IList<IndicatorBase> indicators, int fromTime = 0, int toTime = 0);
    }
}