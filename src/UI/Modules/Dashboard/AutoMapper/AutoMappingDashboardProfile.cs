using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.UI.Module.Dashboard.Model;
using AutoMapper;

namespace AdionFA.UI.Module.Dashboard.AutoMapper
{
    public class AutoMappingDashboardProfile : Profile
    {
        public AutoMappingDashboardProfile()
        {
            CreateMap<ConfigurationDTO, ConfigurationModel>().ReverseMap();

            CreateMap<HistoricalDataDTO, DownloadHistoricalDataModel>().ReverseMap();
            CreateMap<HistoricalDataDTO, UploadHistoricalDataModel>().ReverseMap();

            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleSettingVM>()
                .ForMember(dest => dest.StartDateFormat, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy.MM.dd"))).ReverseMap();
        }
    }
}