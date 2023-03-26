using AdionFA.Infrastructure.Common.Extractor.Model;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Extractor.Contracts
{
    public interface IExtractorService
    {
        List<IndicatorBase> BuildIndicators(string path);
        List<IndicatorBase> BuildIndicatorsFromNode(List<string> node);
        List<IndicatorBase> ExtractorExecute(DateTime fromDate, DateTime toDate, List<IndicatorBase> indicators, IEnumerable<Candle> candles, int timeframeId, decimal variation = 0);
        IEnumerable<Candle> GetCandles(string historyFilePath);
        bool ExtractorWrite(string path, List<IndicatorBase> indicators, int fromTime = 0, int toTime = 0);
    }
}
