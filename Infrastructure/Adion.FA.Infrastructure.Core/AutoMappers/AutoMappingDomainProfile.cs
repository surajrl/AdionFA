using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Aggregates.Core;
using Adion.FA.Core.Domain.Aggregates.Market;
using Adion.FA.Core.Domain.Aggregates.MetaTrader;
using Adion.FA.Core.Domain.Aggregates.Organization;
using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Core.Domain.Aggregates.ReferenceData;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Common;
using Adion.FA.TransferObject.Core;
using Adion.FA.TransferObject.Market;
using Adion.FA.TransferObject.MetaTrader;
using Adion.FA.TransferObject.Organization;
using Adion.FA.TransferObject.Project;
using Adion.FA.TransferObject.ReferenceData;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace Adion.FA.Infrastructure.Core.AutoMappers
{
    public class AutoMappingDomainProfile : Profile
    {
        public AutoMappingDomainProfile()
        {
            //Common
            CreateMap<EntityServiceHost, EntityServiceHostDTO>().ReverseMap();
            CreateMap<EntityType, EntityTypeDTO>().ReverseMap();
            CreateMap<Setting, SettingDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();

            //Core
            CreateMap<CoreUser, CoreUserDTO>().ReverseMap();
            CreateMap<CoreUserType, CoreUserTypeDTO>().ReverseMap();

            //Market
            CreateMap<CurrencyPair, CurrencyPairDTO>().ReverseMap();
            CreateMap<CurrencyPeriod, CurrencyPeriodDTO>().ReverseMap();
            CreateMap<CurrencySpread, CurrencySpreadDTO>().ReverseMap();
            CreateMap<Market, MarketDTO>().ReverseMap();
            CreateMap<MarketData, MarketDataDTO>().ReverseMap();
            CreateMap<MarketDataDetail, MarketDataDetailDTO>().ReverseMap();
            CreateMap<MarketRegion, MarketRegionDTO>().ReverseMap();

            //MetaTrader
            CreateMap<ExpertAdvisor, ExpertAdvisorDTO>().ReverseMap();

            //Organization
            CreateMap<Organization, OrganizationDTO>().ReverseMap();

            //Project
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectConfiguration, ProjectConfigurationDTO>().ReverseMap();
            CreateMap<ProjectGlobalConfiguration, ProjectGlobalConfigurationDTO>().ReverseMap();
            CreateMap<ProjectGlobalScheduleConfiguration, ProjectGlobalScheduleConfigurationDTO>().ReverseMap();
            CreateMap<ProjectScheduleConfiguration, ProjectScheduleConfigurationDTO>().ReverseMap();
            CreateMap<ProjectStep, ProjectStepDTO>().ReverseMap();

            //Reference Data
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
        }
    }
}
