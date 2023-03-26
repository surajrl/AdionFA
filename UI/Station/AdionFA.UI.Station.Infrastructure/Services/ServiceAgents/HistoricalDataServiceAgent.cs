﻿using AdionFA.Core.API.Contracts.Markets;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.TransferObject.Market;
using AdionFA.TransferObject.Base;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.AutoMapper;

using AutoMapper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Services.AppServices
{
    public class HistoricalDataServiceAgent : IHistoricalDataServiceAgent
    {
        public readonly IMapper Mapper;

        public HistoricalDataServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        // Historical Data

        public async Task<IList<HistoricalDataVM>> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                IList<HistoricalDataDTO> all = Array.Empty<HistoricalDataDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<IHistoricalDataAPI>().GetAllHistoricalData(includeGraph);
                });

                return Mapper.Map<IList<HistoricalDataDTO>, IList<HistoricalDataVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<HistoricalDataVM> GetHistoricalData(int historicalDataId, bool includeGraph = false)
        {
            try
            {
                HistoricalDataDTO dto = null;

                await Task.Run(() =>
                {
                    dto = IoC.Get<IHistoricalDataAPI>().GetHistoricalData(historicalDataId, includeGraph);
                });

                return Mapper.Map<HistoricalDataDTO, HistoricalDataVM>(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<HistoricalDataVM> GetHistoricalData(int marketId = (int)MarketEnum.Forex, int symbolId = (int)CurrencyPairEnum.EURUSD, int timeframeId = (int)TimeframeEnum.H1)
        {
            try
            {
                HistoricalDataDTO dto = null;

                await Task.Run(() =>
                {
                    dto = IoC.Get<IHistoricalDataAPI>().GetHistoricalData(marketId, symbolId, timeframeId);
                });

                return Mapper.Map<HistoricalDataDTO, HistoricalDataVM>(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> CreateHistoricalData(HistoricalDataVM vm)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                var dto = Mapper.Map<HistoricalDataVM, HistoricalDataDTO>(vm);

                await Task.Run(() =>
                {
                    result = IoC.Get<IHistoricalDataAPI>().CreateHistoricalData(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public async Task<IList<TimeframeVM>> GetAllTimeframe(bool includeGraph = false)
        {
            try
            {
                IList<TimeframeDTO> all = Array.Empty<TimeframeDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<IHistoricalDataAPI>().GetAllTimeframe(includeGraph);
                });

                return Mapper.Map<IList<TimeframeDTO>, IList<TimeframeVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<TimeframeVM> GetTimeframe(int timeframeId)
        {
            try
            {
                var timeframeDTO = new TimeframeDTO();

                await Task.Run(() =>
                {
                    timeframeDTO = IoC.Get<IHistoricalDataAPI>().GetTimeframe(timeframeId);
                });

                return Mapper.Map<TimeframeDTO, TimeframeVM>(timeframeDTO);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Symbol

        public async Task<ResponseVM> CreateSymbol(SymbolVM symbolVM)
        {
            try
            {
                var result = new ResponseDTO
                {
                    IsSuccess = false
                };

                var symbolDto = Mapper.Map<SymbolVM, SymbolDTO>(symbolVM);

                await Task.Run(() =>
                {
                    result = IoC.Get<IHistoricalDataAPI>().CreateSymbol(symbolDto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<IList<SymbolVM>> GetAllSymbol(bool includeGraph = false)
        {
            try
            {
                IList<SymbolDTO> all = Array.Empty<SymbolDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<IHistoricalDataAPI>().GetAllSymbol(includeGraph);
                });

                return Mapper.Map<IList<SymbolDTO>, IList<SymbolVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<SymbolVM> GetSymbol(int symbolId)
        {
            try
            {
                var symbolDTO = new SymbolDTO();

                await Task.Run(() =>
                {
                    symbolDTO = IoC.Get<IHistoricalDataAPI>().GetSymbol(symbolId);
                });

                return Mapper.Map<SymbolDTO, SymbolVM>(symbolDTO);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<SymbolVM> GetSymbol(string symbolName)
        {
            try
            {
                var symbolDTO = new SymbolDTO();

                await Task.Run(() =>
                {
                    symbolDTO = IoC.Get<IHistoricalDataAPI>().GetSymbol(symbolName);
                });

                return Mapper.Map<SymbolDTO, SymbolVM>(symbolDTO);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
