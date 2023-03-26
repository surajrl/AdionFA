﻿using AdionFA.Core.API.Contracts.Markets;
using AdionFA.Core.Application.Contracts.Markets;
using AdionFA.Core.Domain.Aggregates.Market;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Market;
using System.Collections.Generic;

namespace AdionFA.Core.API.Markets
{
    public class HistoricalDataAPI : IHistoricalDataAPI
    {
        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false)
        {
            using var service = IoC.Get<IHistoricalDataAppService>();
            return service.GetAllHistoricalData(includeGraph);
        }

        public HistoricalDataDTO GetHistoricalData(int marketDataId, bool includeGraph = false)
        {
            using var service = IoC.Get<IHistoricalDataAppService>();
            return service.GetHistoricalData(marketDataId, includeGraph);
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int currencyPeriodId)
        {
            using var service = IoC.Get<IHistoricalDataAppService>();
            return service.GetHistoricalData(marketId, symbolId, currencyPeriodId);
        }

        public ResponseDTO CreateHistoricalData(HistoricalDataDTO market)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateHistoricalData(market);
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe(bool includeGraph = false)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetAllTimeframe(includeGraph);
            }
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetTimeframe(timeframeId);
            }
        }

        // Symbol

        public IList<SymbolDTO> GetAllSymbol(bool includeGraph = false)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetAllSymbol(includeGraph);
            }
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetSymbol(symbolId);
            }
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetSymbol(symbolName);
            }
        }

        public ResponseDTO CreateSymbol(SymbolDTO symbol)
        {
            using (var service = IoC.Get<IHistoricalDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateSymbol(symbol);
            }
        }
    }
}
