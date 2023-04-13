using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum MathOperatorEnum
    {
        [Metadata("GreaterThanOrEqual", ">=", ">=", "GreaterThanOrEqual")]
        GreaterThanOrEqual = 0,

        [Metadata("LessThanOrEqual", "<=", "<=", "LessThanOrEqual")]
        LessThanOrEqual = 1,

        [Metadata("GreaterThan", ">", ">", "GreaterThan")]
        GreaterThan = 2,

        [Metadata("LessThan", "<", "<", "LessThan")]
        LessThan = 3,

        [Metadata("Equal", "=", "=", "Equal")]
        Equal = 4
    }
}
