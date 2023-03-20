using Adion.FA.Infrastructure.Enums.Attributes;

namespace Adion.FA.Infrastructure.Enums
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
