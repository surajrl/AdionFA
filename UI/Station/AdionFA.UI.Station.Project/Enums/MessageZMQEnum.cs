using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.UI.Station.Project.Enums
{
    public enum MessageZMQPutTypeEnum
    {
        [Metadata("Input", "Input", resourceType: typeof(CommonResources))]
        Input = 1,

        [Metadata("Output", "Output", resourceType: typeof(CommonResources))]
        Output = 2,
    }

    public enum MessageZMQPositionTypeEnum
    {
        [Metadata("Buy", "Buy", resourceType: typeof(CommonResources))]
        Buy = 1,

        [Metadata("Sell", "Sell", resourceType: typeof(CommonResources))]
        Sell = 2,
    }
}
