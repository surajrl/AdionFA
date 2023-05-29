using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
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
using AdionFA.UI.Station.Infrastructure.Model.Weka;
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

            // Market Data

            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();

            // Project

            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectGlobalConfigurationDTO, ProjectGlobalConfigurationVM>().ReverseMap();
            CreateMap<ProjectGlobalScheduleConfigurationDTO, ProjectGlobalScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();

            // Reference Data

            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();

            // Extractor & Strategy Builder
            CreateMap<ProjectConfigurationVM, ProjectSettingsModel>().ReverseMap();

            CreateMap<ConfigurationBaseDTO, ConfigurationBaseVM>().ReverseMap();

            CreateMap<REPTreeOutputModel, REPTreeOutputVM>().ReverseMap();
            CreateMap<REPTreeNodeModel, REPTreeNodeVM>().ReverseMap();

            // Assembled Builder

            CreateMap<AssembledBuilderModel, AssembledBuilderVM>().ReverseMap();
        }
    }
}