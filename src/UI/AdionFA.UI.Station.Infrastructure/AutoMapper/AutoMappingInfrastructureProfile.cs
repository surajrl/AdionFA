using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Project;

using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.Common;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Infrastructure.Model.Project;
using AutoMapper;

namespace AdionFA.UI.Infrastructure.AutoMapper
{
    public class AutoMappingInfrastructureProfile : Profile
    {
        public AutoMappingInfrastructureProfile()
        {
            // Base

            CreateMap<ResponseVM, ResponseDTO>().ReverseMap();
            CreateMap<ConfigurationBaseVM, ConfigurationBaseDTO>().ReverseMap();

            // Common

            CreateMap<SettingDTO, SettingVM>().ReverseMap();
            CreateMap<GlobalConfigurationDTO, GlobalConfigurationVM>().ReverseMap();
            CreateMap<GlobalScheduleConfigurationDTO, GlobalScheduleConfigurationVM>().ReverseMap();

            // Market Data

            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<SymbolDTO, SymbolVM>().ReverseMap();
            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleVM>().ReverseMap();

            // Project

            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();
        }
    }
}