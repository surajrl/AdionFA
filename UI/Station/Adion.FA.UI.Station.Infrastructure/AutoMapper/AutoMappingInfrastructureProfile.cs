using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Common;
using Adion.FA.TransferObject.Core;
using Adion.FA.TransferObject.Market;
using Adion.FA.TransferObject.Organization;
using Adion.FA.TransferObject.Project;
using Adion.FA.TransferObject.ReferenceData;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using Adion.FA.UI.Station.Infrastructure.Model.Core;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Organization;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.Model.ReferenceData;
using AutoMapper;

namespace Adion.FA.UI.Station.Infrastructure.AutoMapper
{
    public class AutoMappingInfrastructureProfile : Profile
    {
        public AutoMappingInfrastructureProfile()
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
        }
    }
}
