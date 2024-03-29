﻿using AdionFA.Infrastructure.Extractor.Model;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Extractor.Contracts
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
            int minimumVariation);

        IList<IndicatorBase> CalculateNodeIndicators(
            Candle firstCandle,
            Candle currentCandle,
            IList<IndicatorBase> indicators,
            IEnumerable<Candle> candleHistory);

        IList<Candle> GetCandles(string historyFilePath);

        bool ExtractorWrite(string path, IList<IndicatorBase> indicators);
    }
}