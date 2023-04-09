using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Contracts.MarketData;
using AdionFA.Core.Domain.Contracts.Repositories;
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
        public IRepository<HistoricalDataDetail> HistoricalDataDetailRepository { get; set; }
        public IRepository<Timeframe> TimeframeRepository { get; set; }
        public IRepository<Symbol> SymbolRepository { get; set; }

        public MarketDataDomainService(string tenantId, string ownerId, string owner,
            IRepository<HistoricalData> marketDataRepository,
            IRepository<HistoricalDataDetail> marketDataDetailRepository,
            IRepository<Timeframe> timeframeRepository,
            IRepository<Symbol> symbolRepository) : base(tenantId, ownerId, owner)
        {
            HistoricalDataRepository = marketDataRepository;
            HistoricalDataDetailRepository = marketDataDetailRepository;
            TimeframeRepository = timeframeRepository;
            SymbolRepository = symbolRepository;
        }

        // Historical Data

        public IList<HistoricalData> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                Expression<Func<HistoricalData, dynamic>>[] includes = { md => md.HistoricalDataDetails };

                IList<HistoricalData> result = HistoricalDataRepository.GetAll(includeGraph ? includes : null).ToList();

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
                Expression<Func<HistoricalData, bool>> predicate = hd => hd.HistoricalDataId == historicalDataId;

                HistoricalData h = includeGraph ?
                    HistoricalDataRepository.FirstOrDefault(predicate, hd => hd.HistoricalDataDetails)
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
                HistoricalData h = HistoricalDataRepository.GetAll(
                        h => h.MarketId == marketId &&
                        h.SymbolId == symbolId &&
                        h.TimeframeId == timeframeId,
                        h => h.HistoricalDataDetails
                    ).FirstOrDefault();

                return h;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateHistoricalData(HistoricalData market)
        {
            try
            {
                market.StartDate = DateTime.UtcNow;

                foreach (var psch in market.HistoricalDataDetails)
                {
                    psch.IsDeleted = false;
                    psch.Inaccesible = false;
                    psch.TenantId = _tenantId;
                    psch.CreatedById = _ownerId;
                    psch.CreatedByUserName = _owner;
                    psch.CreatedOn = DateTime.UtcNow;
                }

                HistoricalDataRepository.Create(market);

                return market.HistoricalDataId;
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
