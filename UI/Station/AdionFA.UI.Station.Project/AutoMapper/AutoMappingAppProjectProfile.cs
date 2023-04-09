using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AutoMapper;
using AdionFA.UI.Station.Project.Model.Configuration;
using AdionFA.TransferObject.Base;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using AdionFA.UI.Station.Project.Model.AssembledBuilder;
using AdionFA.TransferObject.Common;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.TransferObject.Core;
using AdionFA.UI.Station.Infrastructure.Model.Core;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Organization;
using AdionFA.UI.Station.Infrastructure.Model.Organization;
using AdionFA.TransferObject.Project;
using AdionFA.TransferObject.ReferenceData;
using AdionFA.UI.Station.Infrastructure.Model.ReferenceData;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.UI.Station.Infrastructure.Model.Weka;

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

            // Core
            CreateMap<CoreUserDTO, CoreUserVM>().ReverseMap();
            CreateMap<CoreUserTypeDTO, CoreUserTypeVM>().ReverseMap();

            // Market Data
            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<CurrencySpreadDTO, CurrencySpreadVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataDetailDTO, HistoricalDataDetailVM>().ReverseMap();
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

            // Reference Data
            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();

            // Extractor & Strategy Builder
            CreateMap<ProjectConfigurationVM, ProjectSettingsModel>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationVM, ProjectScheduleConfigurationVM>().ReverseMap();

            CreateMap<ConfigurationBaseDTO, ConfigurationBaseVM>().ReverseMap();
            CreateMap<AutoAdjustConfigModel, ConfigurationBaseVM>().ReverseMap();

            CreateMap<REPTreeOutputModel, REPTreeOutputVM>().ReverseMap();
            CreateMap<REPTreeNodeModel, REPTreeNodeVM>().ReverseMap();

            // Assembled Builder
            CreateMap<AssembledBuilderModel, AssembledBuilderBindableModel>().ReverseMap();
            CreateMap<NodeAssembledModel, NodeAssembledBindableModel>().ReverseMap();
            CreateMap<StartNodeAssembledModel, StartNodeAssembledBindableModel>().ReverseMap();
            CreateMap<EndNodeAssembledModel, EndNodeAssembledBindableModel>().ReverseMap();
            CreateMap<BacktestNodeAssembledModel, BacktestNodeAssembledBindableModel>().ReverseMap();
        }
    }
}
