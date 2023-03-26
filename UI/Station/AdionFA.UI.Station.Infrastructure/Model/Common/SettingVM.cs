using AdionFA.UI.Station.Infrastructure.Model.Base;

namespace AdionFA.UI.Station.Infrastructure.Model.Common
{
    public class SettingVM : EntityBaseVM
    {
        public int SettingId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
