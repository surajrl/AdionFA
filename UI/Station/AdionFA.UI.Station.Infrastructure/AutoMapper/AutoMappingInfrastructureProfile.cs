using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.MetaTrader;
using AdionFA.TransferObject.Project;
using AdionFA.TransferObject.ReferenceData;

using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
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
            CreateMap<ConfigurationBaseVM, ConfigurationBaseDTO>().ReverseMap();

            // Common

            CreateMap<EntityServiceHostDTO, EntityServiceHostVM>().ReverseMap();
            CreateMap<EntityTypeDTO, EntityTypeVM>().ReverseMap();
            CreateMap<SettingDTO, SettingVM>().ReverseMap();
            CreateMap<TransferObject.Common.ConfigurationDTO, ConfigurationVM>().ReverseMap();
            CreateMap<ScheduleConfigurationDTO, ScheduleConfigurationVM>().ReverseMap();

            // Market Data

            CreateMap<SymbolDTO, SymbolVM>().ReverseMap();
            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();

            // Project

            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<TransferObject.Project.ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();

            // MetaTrader

            CreateMap<ExpertAdvisorDTO, ExpertAdvisorVM>().ReverseMap();

            // Reference Data

            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();
        }
    }
}