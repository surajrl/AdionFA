using Adion.FA.Infrastructure.Enums.Attributes;
using Adion.FA.Infrastructure.I18n.Resources;

namespace Adion.FA.Infrastructure.Enums
{
    public enum SettingEnum
    {
        [Metadata("Culture", "Culture", "eng", resourceType: typeof(EnumResources))]
        Culture = 1,

        [Metadata("Theme", "Theme", "Light", resourceType: typeof(EnumResources))]
        Theme = 2,
        
        [Metadata("Color", "Color", "Orange", resourceType: typeof(EnumResources))]
        Color = 3,

        [Metadata("DefaultWorkspace", "DefaultWorkspace", null, resourceType: typeof(EnumResources))]
        DefaultWorkspace = 4,
    }
}
