using Adion.FA.Infrastructure.Common.Weka.Model;
using Adion.FA.UI.Station.Project.Model.StrategyBuilder;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using AutoMapper;
using Adion.FA.UI.Station.Project.Model.Configuration;
using Adion.FA.TransferObject.Base;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using Adion.FA.UI.Station.Project.Model.AssembledBuilder;
using Adion.FA.TransferObject.Common;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using Adion.FA.TransferObject.Core;
using Adion.FA.UI.Station.Infrastructure.Model.Core;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.TransferObject.Market;
using Adion.FA.TransferObject.Organization;
using Adion.FA.UI.Station.Infrastructure.Model.Organization;
using Adion.FA.TransferObject.Project;
using Adion.FA.TransferObject.ReferenceData;
using Adion.FA.UI.Station.Infrastructure.Model.ReferenceData;

namespace Adion.FA.UI.Station.Project.AutoMapper
{
    public class AutoMappingAppProjectProfile : Profile
    {
        public AutoMappingAppProjectProfile()
        {
            //Base
            CreateMap<ResponseVM, ResponseDTO>().ReverseMap();

            //Common
            CreateMap<EntityServiceHostDTO, EntityServiceHostVM>().ReverseMap();
            CreateMap<EntityTypeDTO, EntityTypeVM>().ReverseMap();
            CreateMap<SettingDTO, SettingVM>().ReverseMap();
            CreateMap<StatusDTO, StatusVM>().ReverseMap();

            //Core
            CreateMap<CoreUserDTO, CoreUserVM>().ReverseMap();
            CreateMap<CoreUserTypeDTO, CoreUserTypeVM>().ReverseMap();

            //Market
            CreateMap<CurrencyPairDTO, CurrencyPairVM>().ReverseMap();
            CreateMap<CurrencyPeriodDTO, CurrencyPeriodVM>().ReverseMap();
            CreateMap<CurrencySpreadDTO, CurrencySpreadVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<MarketDataDTO, MarketDataVM>().ReverseMap();
            CreateMap<MarketDataDetailDTO, MarketDataDetailVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();

            //Organization
            CreateMap<OrganizationDTO, OrganizationVM>().ReverseMap();

            //Project
            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectGlobalConfigurationDTO, ProjectGlobalConfigurationVM>().ReverseMap();
            CreateMap<ProjectGlobalScheduleConfigurationDTO, ProjectGlobalScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectStepDTO, ProjectStepVM>().ReverseMap();

            //Reference Data
            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();

            #region Extractor & Strategy Builder
            CreateMap<ProjectConfigurationVM, ProjectConfigurationSettingModel>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationVM, ProjectScheduleConfigurationVM>().ReverseMap();

            CreateMap<ConfigurationBaseDTO, ConfigurationBaseVM>().ReverseMap();
            CreateMap<AutoAdjustConfigModel, ConfigurationBaseVM>().ReverseMap();

            CreateMap<REPTreeOutputModel, REPTreeOutputModelVM>().ReverseMap();
            CreateMap<REPTreeNodeModel, REPTreeNodeModelVM>().ReverseMap();
            #endregion

            #region Assembled Builder
            CreateMap<AssembledBuilderModel, AssembledBuilderBindableModel>().ReverseMap();
            CreateMap<NodeAssembledModel, NodeAssembledBindableModel>().ReverseMap();
            CreateMap<StartNodeAssembledModel, StartNodeAssembledBindableModel>().ReverseMap();
            CreateMap<EndNodeAssembledModel, EndNodeAssembledBindableModel>().ReverseMap();
            CreateMap<BacktestNodeAssembledModel, BacktestNodeAssembledBindableModel>().ReverseMap();
            #endregion
        }
    }
}
