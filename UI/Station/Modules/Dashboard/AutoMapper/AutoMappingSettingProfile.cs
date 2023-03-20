using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using AutoMapper;

namespace Adion.FA.UI.Station.Module.Dashboard.AutoMapper
{
    public class AutoMappingSettingProfile : Profile
    {
        public AutoMappingSettingProfile()
        {
            CreateMap<ProjectGlobalConfigurationVM, ProjectGlobalConfigModel>().ReverseMap();
            CreateMap<ProjectGlobalScheduleConfigurationVM, ProjectGlobalScheduleConfigurationVM>().ReverseMap();

            CreateMap<MarketDataVM, UploadMarketDataModel>().ReverseMap();
            CreateMap<MarketDataDetailVM, MarketDataDetailSettingVM>()
                .ForMember(dest => dest.StartDateFormat, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy.MM.dd"))).ReverseMap();
        }
    }
}
