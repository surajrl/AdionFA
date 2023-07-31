using AdionFA.Application.Contracts;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Extensions;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Application.Services.MarketData
{
    public class MarketDataService : AppServiceBase, IMarketDataService
    {

        public MarketDataService()
            : base()
        {
        }

        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph)
        {
            using var dbContext = new AdionFADbContext();

            var allHistoricalData = new List<HistoricalData>();

            if (includeGraph)
            {
                allHistoricalData = dbContext.Set<HistoricalData>()
                    .Where(e => !e.IsDeleted)
                    .IncludeMultiple(
                    e => e.Market,
                    e => e.Symbol,
                    e => e.Timeframe,
                    e => e.HistoricalDataCandles)
                    .ToList();
            }
            else
            {
                allHistoricalData = dbContext.Set<HistoricalData>()
                    .Where(e => !e.IsDeleted)
                    .ToList();
            }

            return Mapper.Map<IList<HistoricalDataDTO>>(allHistoricalData);
        }

        public HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph)
        {
            using var dbContext = new AdionFADbContext();

            var historicalData = includeGraph
                ? dbContext.Set<HistoricalData>()
                .Where(e => e.HistoricalDataId == historicalDataId && !e.IsDeleted)
                .IncludeMultiple(e => e.Market, e => e.Symbol, e => e.Timeframe, e => e.HistoricalDataCandles)
                .FirstOrDefault()
                : dbContext.Set<HistoricalData>()
                .Where(e => e.HistoricalDataId == historicalDataId && !e.IsDeleted)
                .FirstOrDefault();

            return Mapper.Map<HistoricalDataDTO>(historicalData);
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId, bool includeGraph)
        {
            using var dbContext = new AdionFADbContext();
            var historicalData = includeGraph

                ? dbContext.Set<HistoricalData>()
                .Where(e => e.MarketId == marketId && e.SymbolId == symbolId && e.TimeframeId == timeframeId && !e.IsDeleted)
                .IncludeMultiple(e => e.Market, e => e.Symbol, e => e.Timeframe, e => e.HistoricalDataCandles)
                .FirstOrDefault()
                : dbContext.Set<HistoricalData>()
                .Where(e => e.MarketId == marketId && e.SymbolId == symbolId && e.TimeframeId == timeframeId && !e.IsDeleted)
                .FirstOrDefault();

            return Mapper.Map<HistoricalDataDTO>(historicalData);
        }

        public ResponseDTO CreateHistoricalData(HistoricalDataDTO historicalDataDTO)
        {
            Logger.Information($"MarketDataService.CreateHistoricalData() :: Call.");

            using var dbContext = new AdionFADbContext();

            var responseDTO = new ResponseDTO
            {
                IsSuccess = false
            };

            var isCreated = dbContext.Set<HistoricalData>()
                .Any(e => !e.IsDeleted
                && e.SymbolId == historicalDataDTO.SymbolId
                && e.TimeframeId == historicalDataDTO.TimeframeId
                && e.MarketId == historicalDataDTO.MarketId);

            // Soft delete historical data records with the same market, timeframe and symbol
            if (isCreated)
            {
                Logger.Information($"MarketDataService.CreateHistoricalData() :: Historical data for Market ID {historicalDataDTO.MarketId}, Symbol ID {historicalDataDTO.TimeframeId} and Timeframe ID {historicalDataDTO.TimeframeId} already exists and will be soft deleted.");

                var existingHd = dbContext.Set<HistoricalData>()
                    .Where(e => !e.IsDeleted
                    && e.SymbolId == historicalDataDTO.SymbolId
                    && e.TimeframeId == historicalDataDTO.TimeframeId
                    && e.MarketId == historicalDataDTO.MarketId)
                    .ToList();

                foreach (var hd in existingHd)
                {
                    hd.UpdatedOn = DateTime.UtcNow;
                    hd.IsDeleted = true;

                    dbContext.Set<HistoricalData>().Update(hd);

                    Logger.Information($"MarketDataService.CreateHistoricalData() :: Historical data with ID {hd.HistoricalDataId} updated.");
                }
            }

            var historicalData = Mapper.Map<HistoricalData>(historicalDataDTO);

            foreach (var candle in historicalData.HistoricalDataCandles)
            {
                // Most of the time spend here ...

                candle.CreatedOn = DateTime.UtcNow;
                candle.IsDeleted = false;
            }

            // Add

            historicalData.CreatedOn = DateTime.UtcNow;
            historicalData.IsDeleted = false;
            dbContext.Set<HistoricalData>().Add(historicalData);
            Logger.Information($"MarketDataService.CreateHistoricalData() :: dbContext.Set<HistoricalData>().Add().");

            dbContext.SaveChanges(); // ... and here
            Logger.Information($"MarketDataService.CreateHistoricalData() :: dbContext.SaveChanges().");

            responseDTO.IsSuccess = historicalData.HistoricalDataId > 0;

            return responseDTO;
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe()
        {
            using var dbContext = new AdionFADbContext();
            var allTimeframe = dbContext.Set<Timeframe>()
                .Where(e => !e.IsDeleted);

            return Mapper.Map<IList<TimeframeDTO>>(allTimeframe);
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            using var dbContext = new AdionFADbContext();
            var timeframe = dbContext.Set<Timeframe>()
                .FirstOrDefault(e => e.TimeframeId == timeframeId && !e.IsDeleted);

            return Mapper.Map<TimeframeDTO>(timeframe);
        }

        // Symbol

        public ResponseDTO CreateSymbol(SymbolDTO symbolDTO)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false,
            };

            var symbol = Mapper.Map<Symbol>(symbolDTO);

            // Check if the symbol already exists
            if (dbContext.Set<Symbol>().Any(e => e.Name == symbol.Name && !e.IsDeleted))
            {
                response.IsSuccess = true;
                return response;
            }

            // Add

            symbol.CreatedOn = DateTime.UtcNow;
            symbol.IsDeleted = false;

            dbContext.Set<Symbol>().Add(symbol);
            dbContext.SaveChanges();

            response.IsSuccess = symbol.SymbolId > 0;

            return response;
        }

        public IList<SymbolDTO> GetAllSymbol()
        {
            using var dbContext = new AdionFADbContext();
            var allSymbol = dbContext.Set<Symbol>()
                .Where(e => !e.IsDeleted);

            return Mapper.Map<IList<SymbolDTO>>(allSymbol);
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            using var dbContext = new AdionFADbContext();
            var symbol = dbContext.Set<Symbol>()
                .FirstOrDefault(e => e.SymbolId == symbolId && !e.IsDeleted);

            return Mapper.Map<SymbolDTO>(symbol);
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            using var dbContext = new AdionFADbContext();
            var symbol = dbContext.Set<Symbol>()
                .FirstOrDefault(e => e.Name == symbolName && !e.IsDeleted);

            return Mapper.Map<SymbolDTO>(symbol);
        }
    }
}
