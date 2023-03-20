using Adion.FA.Core.Domain.Aggregates.Market;
using Adion.FA.Core.Domain.Contracts.Markets;
using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.Infrastructure.Core.Data.Repositories.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Adion.FA.Core.Domain.Services.Markets
{
    public class MarketDataDomainService : DomainServiceBase, IMarketDataDomainService
    {
        #region Repositories
        public IRepository<MarketData> MarketDataRepository { get; set; }
        public IRepository<MarketDataDetail> MarketDataDetailRepository { get; set; }
        #endregion

        #region Ctor
        public MarketDataDomainService(string tenantId, string ownerId, string owner,
            IRepository<MarketData> marketDataRepository,
            IRepository<MarketDataDetail> marketDataDetailRepository) : base(tenantId, ownerId, owner) 
        {
            MarketDataRepository = marketDataRepository;
            MarketDataDetailRepository = marketDataDetailRepository;
        }
        #endregion

        #region Market Data
        public IList<MarketData> GetAllMarketData(bool includeGraph = false)
        {
            try
            {
                Expression<Func<MarketData, bool>> projection = md => (md.EndDate ?? DateTime.MinValue) == DateTime.MinValue;
                Expression<Func<MarketData, dynamic>> [] includes = { md => md.MarketDataDetails };

                IList<MarketData> result = MarketDataRepository.GetAll(projection, includeGraph ? includes : null).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public MarketData GetMarketData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                Expression<Func<MarketData, bool>> predicate =
                    _md => _md.MarketDataId == marketDataId && (_md.EndDate ?? DateTime.MinValue) == DateTime.MinValue;

                MarketData h = includeGraph ? MarketDataRepository.FirstOrDefault(predicate,
                        _h => _h.MarketDataDetails
                    ) : MarketDataRepository.FirstOrDefault(predicate);

                return h;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public MarketData GetMarketData(int marketId, int currencyPairId, int currencyPeriodId)
        {
            try
            {
                MarketData h = MarketDataRepository.GetAll(
                        h => h.MarketId == marketId && h.CurrencyPairId == currencyPairId && h.CurrencyPeriodId == currencyPeriodId
                             && h.EndDate == null,
                        h => h.MarketDataDetails
                    ).FirstOrDefault();

                return h;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateMarketData(MarketData market)
        {
            try
            {
                //Close temporal record
                MarketDataRepository.CloseTemporalRecord();

                market.StartDate = DateTime.UtcNow;

                foreach (var psch in market.MarketDataDetails)
                {
                    psch.IsDeleted = false;
                    psch.Inaccesible = false;
                    psch.TenantId = _tenantId;
                    psch.CreatedById = _ownerId;
                    psch.CreatedByUserName = _owner;
                    psch.CreatedOn = DateTime.UtcNow;
                }
                MarketDataRepository.Create(market);

                return market.MarketDataId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion
    }
}
