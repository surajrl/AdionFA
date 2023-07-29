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
using System.Threading.Tasks;

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

        public async Task<ResponseDTO> CreateHistoricalDataAsync(HistoricalDataDTO historicalDataDTO)
        {
            using var dbContext = new AdionFADbContext();
            var responseDTO = new ResponseDTO
            {
                IsSuccess = false
            };

            var isCreated = dbContext.Set<HistoricalData>()
                .Any(e => e.MarketId == historicalDataDTO.MarketId
                && e.SymbolId == historicalDataDTO.SymbolId
                && e.TimeframeId == historicalDataDTO.TimeframeId
                && !e.IsDeleted);

            // Soft delete historical data records with the same market, timeframe and symbol
            if (isCreated)
            {
                var existing = dbContext.Set<HistoricalData>()
                    .Where(e => e.MarketId == historicalDataDTO.MarketId
                    && e.SymbolId == historicalDataDTO.SymbolId
                    && e.TimeframeId == historicalDataDTO.TimeframeId
                    && !e.IsDeleted)
                    .ToList();

                foreach (var hd in existing)
                {
                    hd.UpdatedOn = DateTime.UtcNow;
                    hd.IsDeleted = true;
                }

                dbContext.Set<HistoricalData>().UpdateRange(existing);
            }

            var historicalData = Mapper.Map<HistoricalData>(historicalDataDTO);

            foreach (var candle in historicalData.HistoricalDataCandles)
            {
                candle.CreatedById = Id;
                candle.CreatedByUserName = Username;
                candle.CreatedOn = DateTime.UtcNow;

                candle.IsDeleted = false;
            }

            // Set to null so that it does not try and create them again
            historicalData.Market = null;
            historicalData.Symbol = null;
            historicalData.Timeframe = null;

            dbContext.Set<HistoricalData>().Add(historicalData);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            responseDTO.IsSuccess = historicalData.HistoricalDataId > 0;

            return responseDTO;
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe()
        {
            using var dbContext = new AdionFADbContext();
            return Mapper.Map<IList<TimeframeDTO>>(dbContext.Set<Timeframe>().Where(e => !e.IsDeleted));
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            using var dbContext = new AdionFADbContext();
            return Mapper.Map<TimeframeDTO>(dbContext.Set<Timeframe>().FirstOrDefault(e => e.TimeframeId == timeframeId && !e.IsDeleted));
        }

        // Symbol

        public async Task<ResponseDTO> CreateSymbolAsync(SymbolDTO symbolDTO)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false,
            };

            var symbol = Mapper.Map<Symbol>(symbolDTO);

            // Check if the symbol already exists
            if (dbContext.Set<Symbol>().Any(e => e.SymbolId == symbol.SymbolId && !e.IsDeleted))
            {
                response.IsSuccess = true;
                return response;
            }

            dbContext.Set<Symbol>().Add(symbol);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            response.IsSuccess = symbol.SymbolId > 0;

            return response;
        }

        public IList<SymbolDTO> GetAllSymbol()
        {
            using var dbContext = new AdionFADbContext();
            return Mapper.Map<IList<SymbolDTO>>(dbContext.Set<Symbol>().Where(e => !e.IsDeleted));
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            using var dbContext = new AdionFADbContext();
            return Mapper.Map<SymbolDTO>(dbContext.Set<Symbol>().FirstOrDefault(e => e.SymbolId == symbolId && !e.IsDeleted));
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            using var dbContext = new AdionFADbContext();
            return Mapper.Map<SymbolDTO>(dbContext.Set<Symbol>().FirstOrDefault(e => e.Name == symbolName && !e.IsDeleted));
        }
    }
}
