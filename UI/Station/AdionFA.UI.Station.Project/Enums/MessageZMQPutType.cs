using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.UI.Station.Project.Enums
{
    public enum MessageZMQPutType
    {
        [Metadata("Input", "Input", resourceType: typeof(CommonResources))]
        Input = 1,

        [Metadata("Output", "Output", resourceType: typeof(CommonResources))]
        Output = 2,
    }
}
