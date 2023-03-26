using AdionFA.TransferObject.Base;

namespace AdionFA.TransferObject.Common
{
    public class SettingDTO : EntityBaseDTO
    {
        public int SettingId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
