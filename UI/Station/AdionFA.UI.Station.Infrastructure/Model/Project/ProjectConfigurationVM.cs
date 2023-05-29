using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectConfigurationVM : ConfigurationBaseVM
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectVM Project { get; set; }

        public int? HistoricalDataId { get; set; }
        public HistoricalDataVM HistoricalData { get; set; }

        public string HistoricalDataName => HistoricalData?.Description != null ?
            HistoricalData.Description
            : $"{((CurrencyPairEnum)HistoricalData.SymbolId).GetMetadata().Name} - {((TimeframeEnum)HistoricalData.TimeframeId).GetMetadata().Name}";

        public bool IsFavorite { get; set; }
        public string WorkspacePath { get; set; }
        public IList<ProjectScheduleConfigurationVM> ProjectScheduleConfigurations { get; set; }
    }
}