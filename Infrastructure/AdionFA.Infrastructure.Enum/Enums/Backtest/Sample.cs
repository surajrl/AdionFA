using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums.Backtest
{
    public enum Sample
    {
        [Metadata(nameKey: "IS", descriptionKey: "In Sample")]
        IS = 0,

        [Metadata(nameKey: "OS", descriptionKey: "Out of Sample")]
        OS = 1
    }
}
