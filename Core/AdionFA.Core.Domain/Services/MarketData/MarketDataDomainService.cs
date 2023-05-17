using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Contracts.MarketData;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.Core.Data.Repositories.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Core.Domain.Services.MarketData
{
    public class MarketDataDomainService : DomainServiceBase, IMarketDataDomainService
    {
        // Repositories

        public IRepository<HistoricalData> HistoricalDataRepository { get; set; }
        public IRepository<HistoricalDataCandle> HistoricalDataCandleRepository { get; set; }
        public IRepository<Timeframe> TimeframeRepository { get; set; }
        public IRepository<Symbol> SymbolRepository { get; set; }

        public MarketDataDomainService(string tenantId, string ownerId, string owner,
            IRepository<HistoricalData> marketDataRepository,
            IRepository<HistoricalDataCandle> marketDataDetailRepository,
            IRepository<Timeframe> timeframeRepository,
            IRepository<Symbol> symbolRepository) : base(tenantId, ownerId, owner)
        {
            HistoricalDataRepository = marketDataRepository;
            HistoricalDataCandleRepository = marketDataDetailRepository;
            TimeframeRepository = timeframeRepository;
            SymbolRepository = symbolRepository;
        }

        // Historical Data

        public IList<HistoricalData> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                Expression<Func<HistoricalData, bool>> projection = hd => (hd.EndDate ?? DateTime.MinValue) == DateTime.MinValue;
                Expression<Func<HistoricalData, dynamic>>[] includes = { hd => hd.HistoricalDataCandles };

                var result = HistoricalDataRepository.GetAll(projection, includeGraph ? includes : null).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalData GetHistoricalData(int historicalDataId, bool includeGraph = false)
        {
            try
            {
                Expression<Func<HistoricalData, bool>> predicate =
                    hd =>
                    hd.HistoricalDataId == historicalDataId &&
                    (hd.EndDate ?? DateTime.MinValue) == DateTime.MinValue;

                var h = includeGraph ?
                    HistoricalDataRepository.FirstOrDefault(predicate, hd => hd.HistoricalDataCandles)
                    : HistoricalDataRepository.FirstOrDefault(predicate);

                return h;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalData GetHistoricalData(int marketId, int symbolId, int timeframeId)
        {
            try
            {
                var h = HistoricalDataRepository.GetAll(
                    h => h.MarketId == marketId &&
                    h.SymbolId == symbolId &&
                    h.TimeframeId == timeframeId &&
                    h.EndDate == null,

                    h => h.HistoricalDataCandles
                    ).FirstOrDefault();

                return h;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateHistoricalData(HistoricalData historicalData)
        {
            try
            {
                var exisitingHd = HistoricalDataRepository.GetAll(hd =>
                hd.MarketId == historicalData.MarketId &&
                hd.TimeframeId == historicalData.TimeframeId &&
                hd.SymbolId == historicalData.SymbolId);

                // Close historical data records with the same market, timeframe and symbol
                if (exisitingHd.Any())
                {
                    foreach (var hd in exisitingHd)
                    {
                        hd.EndDate = DateTime.UtcNow.AddDays(-1);
                        HistoricalDataRepository.Update(hd);
                    }
                }

                historicalData.StartDate = DateTime.UtcNow;

                foreach (var candle in historicalData.HistoricalDataCandles)
                {
                    candle.IsDeleted = false;
                    candle.Inaccesible = false;
                    candle.TenantId = _tenantId;
                    candle.CreatedById = _ownerId;
                    candle.CreatedByUserName = _owner;
                    candle.CreatedOn = DateTime.UtcNow;
                }

                HistoricalDataRepository.Create(historicalData);

                return historicalData.HistoricalDataId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int UpdateHistoricalData(HistoricalData historicalData)
        {
            try
            {
                var hdId = HistoricalDataRepository.FirstOrDefault(hd =>
                hd.MarketId == historicalData.MarketId &&
                hd.SymbolId == historicalData.SymbolId &&
                hd.TimeframeId == historicalData.TimeframeId)?.HistoricalDataId;

                if (hdId != null && hdId > 0)
                {
                    historicalData.HistoricalDataId = (int)hdId;
                    var hdCandles = HistoricalDataCandleRepository.GetAll(candle => candle.HistoricalDataId == hdId);
                    HistoricalDataCandleRepository.Delete(hdCandles);

                    HistoricalDataRepository.Delete(historicalData);
                }

                foreach (var candle in historicalData.HistoricalDataCandles)
                {
                    candle.IsDeleted = false;
                    candle.Inaccesible = false;
                    candle.TenantId = _tenantId;
                    candle.CreatedById = _ownerId;
                    candle.CreatedByUserName = _owner;
                    candle.CreatedOn = DateTime.UtcNow;
                }

                HistoricalDataRepository.Create(historicalData);

                return historicalData.HistoricalDataId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe
        public IList<Timeframe> GetAllTimeframe(bool includeGraph)
        {
            try
            {
                IList<Timeframe> timeframes = TimeframeRepository.GetAll().ToList();
                return timeframes;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public Timeframe GetTimeframe(int timeframeId)
        {
            try
            {
                Expression<Func<Timeframe, bool>> predicate = s => s.TimeframeId == timeframeId;
                var includes = new List<Expression<Func<Timeframe, dynamic>>> { };

                return TimeframeRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Symbol

        public IList<Symbol> GetAllSymbol(bool includeGraph)
        {
            try
            {
                IList<Symbol> result = SymbolRepository.GetAll().ToList();

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public Symbol GetSymbol(int symbolId)
        {
            try
            {
                Expression<Func<Symbol, bool>> predicate = s => s.SymbolId == symbolId;
                var includes = new List<Expression<Func<Symbol, dynamic>>> { };

                return SymbolRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public Symbol GetSymbol(string symbolName)
        {
            try
            {
                Expression<Func<Symbol, bool>> predicate = s => s.Name == symbolName;
                var includes = new List<Expression<Func<Symbol, dynamic>>> { };

                return SymbolRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int? CreateSymbol(Symbol symbol)
        {
            try
            {
                var all = SymbolRepository.GetAll(s => s.Name == symbol.Name);

                if (all.Any())
                    return null;

                SymbolRepository.Create(symbol);
                return symbol.SymbolId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}