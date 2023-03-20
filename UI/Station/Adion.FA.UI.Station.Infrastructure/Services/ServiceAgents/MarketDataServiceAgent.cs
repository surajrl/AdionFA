using Adion.FA.Infrastructure.Enums;
using Adion.FA.TransferObject.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.AutoMapper;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Core.API.Contracts.Markets;
using Adion.FA.TransferObject.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Base;

namespace Adion.FA.UI.Station.Infrastructure.Services.AppServices
{
    public class MarketDataServiceAgent : IMarketDataServiceAgent
    {
        #region AutoMapper
        public readonly IMapper Mapper;
        #endregion

        #region Ctor
        public MarketDataServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }
        #endregion

        public async Task<IList<MarketDataVM>> GetAllMarketData(bool includeGraph = false)
        {
            try
            {
                IList<MarketDataDTO> all = Array.Empty<MarketDataDTO>().ToList();

                await Task.Run(() => {
                    all = IoC.Get<IMarketDataAPI>().GetAllMarketData(includeGraph);
                });

                return Mapper.Map<IList<MarketDataDTO>, IList<MarketDataVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<MarketDataVM> GetMarketData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                MarketDataDTO dto = null;

                await Task.Run(() => {
                    dto = IoC.Get<IMarketDataAPI>().GetMarketData(marketDataId, includeGraph);
                });

                return Mapper.Map<MarketDataDTO, MarketDataVM>(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<MarketDataVM> GetMarketData(
            int marketId = (int)MarketEnum.Forex, int currencyPairId = (int)CurrencyPairEnum.EURUSD, int currencyPeriodId = (int)CurrencyPeriodEnum.H1)
        {
            try
            {
                MarketDataDTO dto = null;

                await Task.Run(() => {
                    dto = IoC.Get<IMarketDataAPI>().GetMarketData(marketId, currencyPairId, currencyPeriodId);
                });

                return Mapper.Map<MarketDataDTO, MarketDataVM>(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> CreateMarketData(MarketDataVM vm)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false};

                var dto = Mapper.Map<MarketDataVM, MarketDataDTO>(vm);

                await Task.Run(() => {
                    result = IoC.Get<IMarketDataAPI>().CreateMarketData(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
