using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Aggregates.Core;
using AdionFA.Core.Domain.Aggregates.Market;
using AdionFA.Core.Domain.Aggregates.MetaTrader;
using AdionFA.Core.Domain.Aggregates.Organization;
using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Core.Domain.Aggregates.ReferenceData;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.Core;
using AdionFA.TransferObject.Market;
using AdionFA.TransferObject.MetaTrader;
using AdionFA.TransferObject.Organization;
using AdionFA.TransferObject.Project;
using AdionFA.TransferObject.ReferenceData;
using AutoMapper;
using System.Runtime.CompilerServices;

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

            // Core
            CreateMap<CoreUser, CoreUserDTO>().ReverseMap();
            CreateMap<CoreUserType, CoreUserTypeDTO>().ReverseMap();

            // Market
            CreateMap<Symbol, SymbolDTO>().ReverseMap();
            CreateMap<CurrencyPair, CurrencyPairDTO>().ReverseMap();
            CreateMap<Timeframe, TimeframeDTO>().ReverseMap();
            CreateMap<CurrencySpread, CurrencySpreadDTO>().ReverseMap();
            CreateMap<Market, MarketDTO>().ReverseMap();
            CreateMap<HistoricalData, HistoricalDataDTO>().ReverseMap();
            CreateMap<HistoricalDataDetail, HistoricalDataDetailDTO>().ReverseMap();
            CreateMap<MarketRegion, MarketRegionDTO>().ReverseMap();

            // MetaTrader
            CreateMap<ExpertAdvisor, ExpertAdvisorDTO>().ReverseMap();

            // Organization
            CreateMap<Organization, OrganizationDTO>().ReverseMap();

            // Project
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectConfiguration, ProjectConfigurationDTO>().ReverseMap();
            CreateMap<ProjectGlobalConfiguration, ProjectGlobalConfigurationDTO>().ReverseMap();
            CreateMap<ProjectGlobalScheduleConfiguration, ProjectGlobalScheduleConfigurationDTO>().ReverseMap();
            CreateMap<ProjectScheduleConfiguration, ProjectScheduleConfigurationDTO>().ReverseMap();
            CreateMap<ProjectStep, ProjectStepDTO>().ReverseMap();

            // Reference Data
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
        }
    }
}