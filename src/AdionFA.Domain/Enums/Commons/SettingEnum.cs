using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum SettingEnum
    {
        [Metadata("Culture", "Culture", "eng")]
        Culture = 1,

        [Metadata("Theme", "Theme", "Light")]
        Theme = 2,

        [Metadata("Color", "Color", "Orange")]
        Color = 3,

        [Metadata("DefaultWorkspace", "DefaultWorkspace", "")]
        DefaultWorkspace = 4,

        [Metadata("Host", "Host", "192.168.50.137")]
        Host = 5,

        [Metadata("Port", "Port", "5555")]
        Port = 6,
    }
}