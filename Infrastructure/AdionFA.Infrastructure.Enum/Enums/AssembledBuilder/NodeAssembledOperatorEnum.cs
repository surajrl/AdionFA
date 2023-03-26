using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum NodeAssembledOperatorEnum
    {
        [Metadata("AND")]
        AND = 1,

        [Metadata("OR")]
        OR = 2,

        [Metadata("XOR")]
        XOR = 3,
    }
}
