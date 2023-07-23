using AdionFA.Application.Contracts;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdionFA.Application.Services.MarketData
{
    public class MarketDataService : AppServiceBase, IMarketDataService
    {
        private readonly IGenericRepository<HistoricalData> _historicalDataRepository;
        private readonly IGenericRepository<Timeframe> _timeframeRepository;
        private readonly IGenericRepository<Symbol> _symbolRepository;

        public MarketDataService(
            IGenericRepository<HistoricalData> historicalDataRepository,
            IGenericRepository<Timeframe> timeframeRepository,
            IGenericRepository<Symbol> symbolRepository)
            : base()
        {
            _historicalDataRepository = historicalDataRepository;
            _timeframeRepository = timeframeRepository;
            _symbolRepository = symbolRepository;
        }

        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph)
        {
            try
            {
                Expression<Func<HistoricalData, bool>> predicate = historicalData => !historicalData.IsDeleted;
                Expression<Func<HistoricalData, dynamic>>[] includes = {
                    historicalData => historicalData.Market,
                    historicalData => historicalData.Symbol,
                    historicalData => historicalData.Timeframe,
                    historicalData => historicalData.HistoricalDataCandles,
                };

                var allHistoricalData = _historicalDataRepository.GetAll(predicate, includeGraph
                    ? includes
                    : null);

                return Mapper.Map<IList<HistoricalDataDTO>>(allHistoricalData);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph)
        {
            try
            {
                var historicalData = includeGraph
                    ? _historicalDataRepository.FirstOrDefault(
                        hd =>
                        hd.HistoricalDataId == historicalDataId,
                        historicalData => historicalData.Market,
                        historicalData => historicalData.Symbol,
                        historicalData => historicalData.Timeframe,
                        historicalData => historicalData.HistoricalDataCandles)
                    : _historicalDataRepository.FirstOrDefault(
                        hd =>
                        hd.HistoricalDataId == historicalDataId);

                return Mapper.Map<HistoricalDataDTO>(historicalData);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId, bool includeGraph)
        {
            try
            {
                var historicalData = includeGraph
                    ?
                    _historicalDataRepository.FirstOrDefault(
                        hd =>
                        hd.MarketId == marketId
                        && hd.SymbolId == symbolId
                        && hd.TimeframeId == timeframeId)
                    : _historicalDataRepository.FirstOrDefault(
                        hd =>
                        hd.MarketId == marketId
                        && hd.SymbolId == symbolId
                        && hd.TimeframeId == timeframeId,
                        historicalData => historicalData.Market,
                        historicalData => historicalData.Symbol,
                        historicalData => historicalData.Timeframe,
                        historicalData => historicalData.HistoricalDataCandles);

                return Mapper.Map<HistoricalDataDTO>(historicalData);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDTO> CreateHistoricalDataAsync(HistoricalDataDTO historicalDataDTO)
        {
            try
            {
                var responseDTO = new ResponseDTO
                {
                    IsSuccess = false
                };

                var historicalData = Mapper.Map<HistoricalData>(historicalDataDTO);

                var existingHistoricalDatas = _historicalDataRepository.GetAll(
                    historicalData =>
                    historicalData.MarketId == historicalData.MarketId
                    && historicalData.TimeframeId == historicalData.TimeframeId
                    && historicalData.SymbolId == historicalData.SymbolId,
                    historicalData => historicalData.HistoricalDataCandles);

                // Soft delete historical data records with the same market, timeframe and symbol
                if (existingHistoricalDatas.Any())
                {
                    foreach (var existingHistoricalData in existingHistoricalDatas)
                    {
                        await _historicalDataRepository.DeleteAsync(existingHistoricalData, true);
                    }
                }

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

                await _historicalDataRepository.CreateAsync(historicalData).ConfigureAwait(false);

                responseDTO.IsSuccess = historicalData.HistoricalDataId > 0;

                return responseDTO;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe()
        {
            try
            {
                return Mapper.Map<IList<TimeframeDTO>>(_timeframeRepository.GetAll());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            try
            {
                return Mapper.Map<TimeframeDTO>(_timeframeRepository.FirstOrDefault(timeframe => timeframe.TimeframeId == timeframeId));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Symbol

        public async Task<ResponseDTO> CreateSymbolAsync(SymbolDTO symbolDTO)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false,
                };

                var symbolEntity = Mapper.Map<Symbol>(symbolDTO);

                // Check if the symbol already exists
                if (_symbolRepository.GetAll(symbol => symbolEntity.Name == symbol.Name).Any())
                {
                    response.IsSuccess = true;
                    return response;
                }

                await _symbolRepository.CreateAsync(symbolEntity).ConfigureAwait(false);

                response.IsSuccess = symbolEntity.SymbolId > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public IList<SymbolDTO> GetAllSymbol()
        {
            try
            {
                return Mapper.Map<IList<SymbolDTO>>(_symbolRepository.GetAll());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            try
            {
                return Mapper.Map<SymbolDTO>(_symbolRepository.FirstOrDefault(symbol => symbol.SymbolId == symbolId));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            try
            {
                return Mapper.Map<SymbolDTO>(_symbolRepository.FirstOrDefault(symbol => symbol.Name == symbolName));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
