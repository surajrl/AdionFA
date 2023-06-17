using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Project;
using AdionFA.TransferObject.ReferenceData;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Model.ReferenceData;
using AdionFA.UI.Station.Project.Model.AssembledBuilder;
using AdionFA.UI.Station.Project.Model.Configuration;
using AutoMapper;

namespace AdionFA.UI.Station.Project.AutoMapper
{
    public class AutoMappingAppProjectProfile : Profile
    {
        public AutoMappingAppProjectProfile()
        {
            // Base

            CreateMap<ResponseVM, ResponseDTO>().ReverseMap();

            // Common

            CreateMap<EntityServiceHostDTO, EntityServiceHostVM>().ReverseMap();
            CreateMap<EntityTypeDTO, EntityTypeVM>().ReverseMap();
            CreateMap<SettingDTO, SettingVM>().ReverseMap();
            CreateMap<ConfigurationDTO, ConfigurationVM>().ReverseMap();
            CreateMap<ScheduleConfigurationDTO, ScheduleConfigurationVM>().ReverseMap();

            // Market Data

            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();

            // Project

            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectConfigurationVM, ProjectConfigurationModel>().ReverseMap();

            // Reference Data

            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();

            // Extractor & Strategy Builder

            CreateMap<ConfigurationDTO, ConfigurationVM>().ReverseMap();
            CreateMap<ProjectConfigurationVM, ConfigurationDTO>().ReverseMap();

            // Assembled Builder

            CreateMap<ProjectConfigurationVM, ConfigurationBaseDTO>().ReverseMap();
            CreateMap<AssembledBuilderModel, AssembledBuilderProcessModel>().ReverseMap();
        }
    }
}