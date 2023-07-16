using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Model.Configuration;
using AutoMapper;

namespace AdionFA.UI.ProjectStation.AutoMapper
{
    public class AutoMappingProjectStationProfile : Profile
    {
        public AutoMappingProjectStationProfile()
        {
            CreateMap<ProjectConfigurationDTO, ProjectConfigurationModel>().ReverseMap();
            CreateMap<ProjectConfigurationVM, ProjectConfigurationModel>().ReverseMap();
            CreateMap<ConfigurationBaseDTO, ProjectConfigurationModel>().ReverseMap();
            CreateMap<ConfigurationBaseVM, ProjectConfigurationModel>().ReverseMap();
        }
    }
}