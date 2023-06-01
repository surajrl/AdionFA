﻿using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
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
            CreateMap<TransferObject.Common.ConfigurationDTO, ConfigurationVM>().ReverseMap();
            CreateMap<ScheduleConfigurationDTO, ScheduleConfigurationVM>().ReverseMap();

            // Market Data

            CreateMap<TimeframeDTO, TimeframeVM>().ReverseMap();
            CreateMap<MarketDTO, MarketVM>().ReverseMap();
            CreateMap<HistoricalDataDTO, HistoricalDataVM>().ReverseMap();
            CreateMap<HistoricalDataCandleDTO, HistoricalDataCandleVM>().ReverseMap();
            CreateMap<MarketRegionDTO, MarketRegionVM>().ReverseMap();

            // Project

            CreateMap<ProjectDTO, ProjectVM>().ReverseMap();
            CreateMap<TransferObject.Project.ProjectConfigurationDTO, ProjectConfigurationVM>().ReverseMap();
            CreateMap<ProjectScheduleConfigurationDTO, ProjectScheduleConfigurationVM>().ReverseMap();
            CreateMap<ProjectConfigurationVM, ProjectConfigurationModel>().ReverseMap();

            // Reference Data

            CreateMap<CurrencyDTO, CurrencyVM>().ReverseMap();

            // Extractor & Strategy Builder
            CreateMap<TransferObject.Common.ConfigurationDTO, ConfigurationVM>().ReverseMap();
            CreateMap<ProjectConfigurationVM, TransferObject.Common.ConfigurationDTO>().ReverseMap();

            CreateMap<REPTreeOutputModel, REPTreeOutputVM>().ReverseMap();
            CreateMap<REPTreeNodeModel, REPTreeNodeVM>().ReverseMap();

            // Assembled Builder

            CreateMap<ProjectConfigurationVM, ConfigurationBaseDTO>().ReverseMap();
            CreateMap<AssembledBuilderModel, AssembledBuilderVM>().ReverseMap();
        }
    }
}