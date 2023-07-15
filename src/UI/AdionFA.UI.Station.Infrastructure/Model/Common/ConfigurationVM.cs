using AdionFA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.Common
{
    public class ConfigurationVM : ConfigurationBaseVM
    {

        public int ConfigurationId { get; set; }

        public IList<ScheduleConfigurationVM> ScheduleConfigurations { get; set; }

    }
}