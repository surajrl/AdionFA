using AdionFA.Application.Contracts.MarketData;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Application.Services.MarketData
{
    public class MarketDataAppService : AppServiceBase, IMarketDataAppService
    {
        private readonly string _ownerId = "0";
        private readonly string _owner = "admin";

        private readonly IGenericRepository<HistoricalData> _historicalDataRepository;
        private readonly IGenericRepository<HistoricalDataCandle> _historicalDataCandleRepository;
        private readonly IGenericRepository<Timeframe> _timeframeRepository;
        private readonly IGenericRepository<Symbol> _symbolRepository;

        public MarketDataAppService(
            IGenericRepository<HistoricalData> historicalDataRepository,
            IGenericRepository<HistoricalDataCandle> historicalDataCandleRepository,
            IGenericRepository<Timeframe> timeframeRepository,
            IGenericRepository<Symbol> symbolRepository) : base()
        {
            _historicalDataRepository = historicalDataRepository;
            _historicalDataCandleRepository = historicalDataCandleRepository;
            _timeframeRepository = timeframeRepository;
            _symbolRepository = symbolRepository;
        }

        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                Expression<Func<HistoricalData, bool>> projection = hd => (hd.EndDate ?? DateTime.MinValue) == DateTime.MinValue;
                Expression<Func<HistoricalData, dynamic>>[] includes = { hd => hd.HistoricalDataCandles };

                var allHistoricalData = _historicalDataRepository.GetAll(projection, includeGraph ? includes : null).ToList();

                return Mapper.Map<IList<HistoricalDataDTO>>(allHistoricalData);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph = false)
        {
            try
            {
                Expression<Func<HistoricalData, bool>> predicate =
                    hd =>
                    hd.HistoricalDataId == historicalDataId &&
                    (hd.EndDate ?? DateTime.MinValue) == DateTime.MinValue;

                var historicalData = includeGraph
                    ? _historicalDataRepository.FirstOrDefault(predicate, hd => hd.HistoricalDataCandles)
                    : _historicalDataRepository.FirstOrDefault(predicate);

                return Mapper.Map<HistoricalDataDTO>(historicalData);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId)
        {
            try
            {
                var historicalData = _historicalDataRepository.GetAll(
                    h =>
                    h.MarketId == marketId &&
                    h.SymbolId == symbolId &&
                    h.TimeframeId == timeframeId &&
                    h.EndDate == null,
                    h => h.HistoricalDataCandles)
                    .FirstOrDefault();

                return Mapper.Map<HistoricalDataDTO>(historicalData);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateHistoricalData(HistoricalDataDTO market)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var historicalDataEntity = Mapper.Map<HistoricalData>(market);

                var exisitinghHistoricalData = _historicalDataRepository.GetAll(
                    historicalData =>
                    historicalData.MarketId == historicalDataEntity.MarketId &&
                    historicalData.TimeframeId == historicalDataEntity.TimeframeId &&
                    historicalData.SymbolId == historicalDataEntity.SymbolId);

                // Close historical data records with the same market, timeframe and symbol
                if (exisitinghHistoricalData.Any())
                {
                    foreach (var historicalData in exisitinghHistoricalData)
                    {
                        historicalData.EndDate = DateTime.UtcNow.AddDays(-1);
                        _historicalDataRepository.Update(historicalData);
                    }
                }

                historicalDataEntity.StartDate = DateTime.UtcNow;

                foreach (var candle in historicalDataEntity.HistoricalDataCandles)
                {
                    candle.IsDeleted = false;
                    candle.CreatedById = _ownerId;
                    candle.CreatedByUserName = _owner;
                    candle.CreatedOn = DateTime.UtcNow;
                }

                _historicalDataRepository.Create(historicalDataEntity);

                response.IsSuccess = historicalDataEntity.HistoricalDataId > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateHistoricalData(HistoricalDataDTO historicalData)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var entity = Mapper.Map<HistoricalData>(historicalData);

                var historicalDataId = _historicalDataRepository.FirstOrDefault(
                    hd =>
                    hd.MarketId == entity.MarketId
                    && hd.SymbolId == entity.SymbolId
                    && hd.TimeframeId == entity.TimeframeId)?
                    .HistoricalDataId;

                if (historicalDataId != null && historicalDataId > 0)
                {
                    entity.HistoricalDataId = (int)historicalDataId;
                    var hdCandles = _historicalDataCandleRepository.GetAll(candle => candle.HistoricalDataId == historicalDataId);

                    _historicalDataCandleRepository.Delete(hdCandles);
                    _historicalDataRepository.Delete(entity);
                }

                foreach (var candle in historicalData.HistoricalDataCandles)
                {
                    candle.IsDeleted = false;
                    candle.CreatedById = _ownerId;
                    candle.CreatedByUserName = _owner;
                    candle.CreatedOn = DateTime.UtcNow;
                }

                _historicalDataRepository.Create(entity);

                response.IsSuccess = entity.HistoricalDataId > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe(bool includeGraph)
        {
            try
            {
                var allTimeframe = _timeframeRepository.GetAll().ToList();

                return Mapper.Map<IList<TimeframeDTO>>(allTimeframe);
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
                Expression<Func<Timeframe, bool>> predicate = s => s.TimeframeId == timeframeId;
                var includes = new List<Expression<Func<Timeframe, dynamic>>> { };

                var timeframe = _timeframeRepository.FirstOrDefault(predicate, includes.ToArray());

                return Mapper.Map<TimeframeDTO>(timeframe); ;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Symbol

        public ResponseDTO CreateSymbol(SymbolDTO symbolDTO)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false,
                };

                var symbol = Mapper.Map<Symbol>(symbolDTO);

                var all = _symbolRepository.GetAll(symbol => symbol.Name == symbol.Name);

                if (all.Any())
                {
                    return null;
                }

                _symbolRepository.Create(symbol);

                response.IsSuccess = symbol.SymbolId > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public IList<SymbolDTO> GetAllSymbol(bool includeGraph = false)
        {
            try
            {
                var allSymbol = _symbolRepository.GetAll().ToList();

                return Mapper.Map<IList<SymbolDTO>>(allSymbol);
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
                Expression<Func<Symbol, bool>> predicate = s => s.SymbolId == symbolId;
                var includes = new List<Expression<Func<Symbol, dynamic>>> { };

                var symbol = _symbolRepository.FirstOrDefault(predicate, includes.ToArray());

                return Mapper.Map<SymbolDTO>(symbol);
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
                Expression<Func<Symbol, bool>> predicate = s => s.Name == symbolName;
                var includes = new List<Expression<Func<Symbol, dynamic>>> { };

                var symbol = _symbolRepository.FirstOrDefault(predicate, includes.ToArray());

                return Mapper.Map<SymbolDTO>(symbol);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
