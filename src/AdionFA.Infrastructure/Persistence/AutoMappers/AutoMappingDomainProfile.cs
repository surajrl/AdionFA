using AdionFA.Domain.Entities;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.MetaTrader;
using AdionFA.TransferObject.Project;

using AutoMapper;

namespace AdionFA.Infrastructure.Persistance.AutoMappers
{
    public class AutoMappingDomainProfile : Profile
    {
        public AutoMappingDomainProfile()
        {
            // Common

            CreateMap<Setting, SettingDTO>().ReverseMap();
            CreateMap<Configuration, ConfigurationDTO>().ReverseMap();
            CreateMap<ScheduleConfiguration, ScheduleConfigurationDTO>().ReverseMap();

            // Market

            CreateMap<Symbol, SymbolDTO>().ReverseMap();
            CreateMap<Timeframe, TimeframeDTO>().ReverseMap();
            CreateMap<Market, MarketDTO>().ReverseMap();
            CreateMap<HistoricalData, HistoricalDataDTO>().ReverseMap();
            CreateMap<HistoricalDataCandle, HistoricalDataCandleDTO>().ReverseMap();
            CreateMap<MarketRegion, MarketRegionDTO>().ReverseMap();

            // MetaTrader

            CreateMap<ExpertAdvisor, ExpertAdvisorDTO>().ReverseMap();

            // Project

            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectConfiguration, ProjectConfigurationDTO>().ReverseMap();
            CreateMap<ProjectScheduleConfiguration, ProjectScheduleConfigurationDTO>().ReverseMap();
        }
    }
}
