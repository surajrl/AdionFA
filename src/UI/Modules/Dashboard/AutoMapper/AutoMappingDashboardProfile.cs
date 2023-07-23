using AdionFA.TransferObject.MarketData;
using AdionFA.UI.Module.Dashboard.Model;
using AutoMapper;

namespace AdionFA.UI.Module.Dashboard.AutoMapper
{
    public class AutoMappingDashboardProfile : Profile
    {
        public AutoMappingDashboardProfile()
        {
            CreateMap<HistoricalDataDTO, DownloadHistoricalDataModel>().ReverseMap();
            CreateMap<HistoricalDataDTO, UploadHistoricalDataModel>().ReverseMap();
        }
    }
}