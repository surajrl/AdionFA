using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum MathOperatorEnum
    {
        [Metadata("GreaterThanOrEqual", ">=", ">=", "GreaterThanOrEqual")]
        GreaterThanOrEqual,

        [Metadata("LessThanOrEqual", "<=", "<=", "LessThanOrEqual")]
        LessThanOrEqual,

        [Metadata("GreaterThan", ">", ">", "GreaterThan")]
        GreaterThan,

        [Metadata("LessThan", "<", "<", "LessThan")]
        LessThan,

        [Metadata("Equal", "=", "=", "Equal")]
        Equal
    }
}
