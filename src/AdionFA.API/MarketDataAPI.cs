using AdionFA.API.Contracts;
using AdionFA.Application.Contracts.MarketData;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Persistance.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using Ninject;
using System.Collections.Generic;

namespace AdionFA.API
{
    public class MarketDataAPI : IMarketDataAPI
    {
        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IMarketDataAppService>();
            return service.GetAllHistoricalData(includeGraph);
        }

        public HistoricalDataDTO GetHistoricalData(int marketDataId, bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IMarketDataAppService>();
            return service.GetHistoricalData(marketDataId, includeGraph);
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int currencyPeriodId)
        {
            using var service = IoC.Kernel.Get<IMarketDataAppService>();
            return service.GetHistoricalData(marketId, symbolId, currencyPeriodId);
        }

        public ResponseDTO CreateHistoricalData(HistoricalDataDTO historicalData)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateHistoricalData(historicalData);
            }
        }

        public ResponseDTO UpdateHistoricalData(HistoricalDataDTO historicalData)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateHistoricalData(historicalData);
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe(bool includeGraph = false)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetAllTimeframe(includeGraph);
            }
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetTimeframe(timeframeId);
            }
        }

        // Symbol

        public IList<SymbolDTO> GetAllSymbol(bool includeGraph = false)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetAllSymbol(includeGraph);
            }
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetSymbol(symbolId);
            }
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.GetSymbol(symbolName);
            }
        }

        public ResponseDTO CreateSymbol(SymbolDTO symbol)
        {
            using (var service = IoC.Kernel.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateSymbol(symbol);
            }
        }
    }
}
