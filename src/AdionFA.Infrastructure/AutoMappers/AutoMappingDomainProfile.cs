using AdionFA.Domain.Entities;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Project;

using AutoMapper;

namespace AdionFA.Infrastructure.AutoMappers
{
    public class AutoMappingDomainProfile : Profile
    {
        public AutoMappingDomainProfile()
        {
            // Common

            CreateMap<Setting, SettingDTO>().ReverseMap();
            CreateMap<GlobalConfiguration, GlobalConfigurationDTO>().ReverseMap();
            CreateMap<GlobalScheduleConfiguration, GlobalScheduleConfigurationDTO>().ReverseMap();

            // Market

            CreateMap<Symbol, SymbolDTO>().ReverseMap();
            CreateMap<Timeframe, TimeframeDTO>().ReverseMap();
            CreateMap<Market, MarketDTO>().ReverseMap();
            CreateMap<MarketRegion, MarketRegionDTO>().ReverseMap();
            CreateMap<HistoricalData, HistoricalDataDTO>().ReverseMap();
            CreateMap<HistoricalDataCandle, HistoricalDataCandleDTO>().ReverseMap();

            // Project

            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectConfiguration, ProjectConfigurationDTO>().ReverseMap();
            CreateMap<ProjectScheduleConfiguration, ProjectScheduleConfigurationDTO>().ReverseMap();
        }
    }
}
