using AdionFA.Infrastructure.Common.Extractor.Model;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Extractor.Contracts
{
    public interface IExtractorService
    {
        List<IndicatorBase> BuildIndicatorsFromCSV(string path);

        List<IndicatorBase> BuildIndicatorsFromNode(List<string> node);

        List<IndicatorBase> ExtractorExecute(
            DateTime from,
            DateTime to,
            List<IndicatorBase> indicators,
            IEnumerable<Candle> candles,
            int timeframeId,
            decimal variation = 0);

        List<IndicatorBase> ExtractorBacktest(
            Candle firstCandle,
            Candle currentCandle,
            IEnumerable<IndicatorBase> indicators,
            IEnumerable<Candle> candles);

        IEnumerable<Candle> GetCandles(string historyFilePath);

        bool ExtractorWrite(string path, List<IndicatorBase> indicators, int fromTime = 0, int toTime = 0);
    }
}
