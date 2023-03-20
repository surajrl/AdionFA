using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using System.Collections.Generic;

namespace Adion.FA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectConfigurationVM : ConfigurationBaseVM
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectVM Project { get; set; }

        public int? MarketDataId { get; set; }
        public MarketDataVM MarketData { get; set; }

        public string MarketDataName => MarketData?.Description != null ? MarketData.Description :
            $"{((CurrencyPairEnum)MarketData.CurrencyPairId).GetMetadata().Name} - {((CurrencyPeriodEnum)MarketData.CurrencyPeriodId).GetMetadata().Name}";

        public bool IsFavorite { get; set; }
        public string WorkspacePath { get; set; }

        public IList<ProjectScheduleConfigurationVM> ProjectScheduleConfigurations { get; set; }
    }
}
