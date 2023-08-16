using AdionFA.Domain.Entities;
using AdionFA.Domain.Entities.Base;
using AdionFA.Domain.Entities.Configuration;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.Configuration;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Project;

using AutoMapper;

namespace AdionFA.Infrastructure.AutoMappers
{
    public class AutoMappingDomainProfile : Profile
    {
        public AutoMappingDomainProfile()
        {
            // Base

            CreateMap<ConfigurationBase, ConfigurationBaseDTO>().ReverseMap();

            CreateMap<NodeBuilderConfiguration, NodeBuilderConfigurationDTO>().ReverseMap();
            CreateMap<AssemblyBuilderConfiguration, AssemblyBuilderConfigurationDTO>().ReverseMap();
            CreateMap<CrossingBuilderConfiguration, CrossingBuilderConfigurationDTO>().ReverseMap();

            // Common

            CreateMap<Setting, SettingDTO>().ReverseMap();
            CreateMap<GlobalConfiguration, GlobalConfigurationDTO>().ReverseMap();
            CreateMap<GlobalScheduleConfiguration, GlobalScheduleConfigurationDTO>().ReverseMap();

            // Market data

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
