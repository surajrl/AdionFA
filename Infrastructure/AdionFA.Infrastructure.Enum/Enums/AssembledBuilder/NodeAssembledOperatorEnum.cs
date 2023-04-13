using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum NodeAssembledOperatorEnum
    {
        [Metadata(codeKey: "AND")]
        AND = 1,

        [Metadata(codeKey: "OR")]
        OR = 2,

        [Metadata(codeKey: "XOR")]
        XOR = 3,
    }
}
