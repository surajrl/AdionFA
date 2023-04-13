using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.MetaTrader;
using AdionFA.TransferObject.Organization;
using AdionFA.TransferObject.Project;
using AdionFA.TransferObject.ReferenceData;

using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.Core;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
using AdionFA.UI.Station.Infrastructure.Model.Organization;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Model.ReferenceData;

using AutoMapper;

namespace AdionFA.UI.Station.Infrastructure.AutoMapper
{
    public class AutoMappingInfrastructureProfile : Profile
    {
        public AutoMappingInfrastructureProfile()
        {
            // Base
            CreateMap<ResponseVM, ResponseDTO>().ReverseMap();

            // Common
            CreateMap<EntityServiceHostDTO, EntityServiceHostVM>().ReverseMap();
            CreateMap<EntityTypeDTO, EntityTypeVM>().ReverseMap();
            CreateMap<SettingDTO, SettingVM>().ReverseMap();

            // Market Data
            CreateMap<SymbolDTO, SymbolVM>().ReverseMap();
            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<CurrencySpreadDTO, CurrencySpreadVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();

            // Organization
            CreateMap<OrganizationDTO, OrganizationVM>().ReverseMap();

            // Project
            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectGlobalConfigurationDTO, ProjectGlobalConfigurationVM>().ReverseMap();
            CreateMap<ProjectGlobalScheduleConfigurationDTO, ProjectGlobalScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectStepDTO, ProjectStepVM>().ReverseMap();

            // MetaTrader
            CreateMap<ExpertAdvisorDTO, ExpertAdvisorVM>().ReverseMap();

            // Reference Data
            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();
        }
    }
}
