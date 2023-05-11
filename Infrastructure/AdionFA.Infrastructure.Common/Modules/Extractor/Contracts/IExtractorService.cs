using AdionFA.Infrastructure.Common.Extractor.Model;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Extractor.Contracts
{
    public interface IExtractorService
    {
        List<IndicatorBase> BuildIndicatorsFromCSV(string path);

        List<IndicatorBase> BuildIndicatorsFromNode(List<string> node);

        List<IndicatorBase> DoExtraction(
            DateTime from,
            DateTime to,
            List<IndicatorBase> indicators,
            List<Candle> candles,
            int timeframeId,
            decimal variation = 0);

        List<IndicatorBase> DoBacktest(
            Candle firstCandle,
            Candle currentCandle,
            List<IndicatorBase> indicators,
            List<Candle> candleHistory);

        List<Candle> GetCandles(string historyFilePath);

        bool ExtractorWrite(string path, List<IndicatorBase> indicators, int fromTime = 0, int toTime = 0);
    }
}