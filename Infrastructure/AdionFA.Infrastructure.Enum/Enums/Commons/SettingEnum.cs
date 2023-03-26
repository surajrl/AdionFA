using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum SettingEnum
    {
        [Metadata("Culture", "Culture", "eng", resourceType: typeof(EnumResources))]
        Culture = 1,

        [Metadata("Theme", "Theme", "Light", resourceType: typeof(EnumResources))]
        Theme = 2,
        
        [Metadata("Color", "Color", "Orange", resourceType: typeof(EnumResources))]
        Color = 3,

        [Metadata("DefaultWorkspace", "DefaultWorkspace", resourceType: typeof(EnumResources))]
        DefaultWorkspace = 4,

        [Metadata("IPAddress", "IPAddress", resourceType: typeof(EnumResources))]
        IPAddress = 5,
        
        [Metadata("Port", "Port", resourceType: typeof(EnumResources))]
        Port = 6,
    }
}
