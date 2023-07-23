using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum MathOperatorEnum
    {
        [Metadata("GreaterThanOrEqual", ">=", ">=")]
        GreaterThanOrEqual = 0,

        [Metadata("LessThanOrEqual", "<=", "<=")]
        LessThanOrEqual = 1,

        [Metadata("GreaterThan", ">", ">")]
        GreaterThan = 2,

        [Metadata("LessThan", "<", "<")]
        LessThan = 3,

        [Metadata("Equal", "=", "=")]
        Equal = 4
    }
}
