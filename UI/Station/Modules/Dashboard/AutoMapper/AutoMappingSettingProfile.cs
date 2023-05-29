using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AutoMapper;

namespace AdionFA.UI.Station.Module.Dashboard.AutoMapper
{
    public class AutoMappingSettingProfile : Profile
    {
        public AutoMappingSettingProfile()
        {
            CreateMap<ConfigurationVM, ConfigurationModel>().ReverseMap();
            CreateMap<ScheduleConfigurationVM, ScheduleConfigurationVM>().ReverseMap();

            CreateMap<HistoricalDataVM, UploadHistoricalDataModel>().ReverseMap();
            CreateMap<HistoricalDataCandleVM, HistoricalDataCandleSettingVM>()
                .ForMember(dest => dest.StartDateFormat, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy.MM.dd"))).ReverseMap();
        }
    }
}