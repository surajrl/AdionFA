using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Aggregates.MetaTrader;
using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Core.Domain.Aggregates.ReferenceData;

using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.MetaTrader;
using AdionFA.TransferObject.Project;
using AdionFA.TransferObject.ReferenceData;

using AutoMapper;

namespace AdionFA.Infrastructure.Core.AutoMappers
{
    public class AutoMappingDomainProfile : Profile
    {
        public AutoMappingDomainProfile()
        {
            // Common

            CreateMap<EntityServiceHost, EntityServiceHostDTO>().ReverseMap();
            CreateMap<EntityType, EntityTypeDTO>().ReverseMap();
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

            // Reference Data

            CreateMap<Currency, CurrencyDTO>().ReverseMap();
        }
    }
}
