using AdionFA.UI.Infrastructure.Model.Common;
using FluentValidation;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class AssemblyBuilderConfigurationBaseVMValidator : AbstractValidator<AssemblyBuilderConfigurationVM>
    {
        public AssemblyBuilderConfigurationBaseVMValidator()
        {
            RuleFor(m => m.WekaMaxRatio)
            .GreaterThanOrEqualTo(0);

            RuleFor(m => m.WekaNTotal)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.WekaStartDepth)
                .GreaterThanOrEqualTo(1)
                .Must((m, startDepth) => startDepth <= m.WekaEndDepth);

            RuleFor(m => m.WekaEndDepth)
                .GreaterThanOrEqualTo(1)
                .Must((m, endDepth) => endDepth >= m.WekaStartDepth);

            RuleFor(m => m.MinTotalTradesIS)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.MaxSuccessRateImprovementOS)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .Must((m, maxSuccessRateImprovementOS) => maxSuccessRateImprovementOS >= m.MinSuccessRateImprovementIS);

            RuleFor(m => m.MinSuccessRateImprovementOS)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .Must((m, minSuccessRateImprovementOS) => minSuccessRateImprovementOS <= m.MaxSuccessRateImprovementIS);

            RuleFor(m => m.MaxSuccessRateImprovementIS)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .Must((m, maxSuccessRateImprovementIS) => maxSuccessRateImprovementIS >= m.MinSuccessRateImprovementIS);

            RuleFor(m => m.MinSuccessRateImprovementIS)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .Must((m, minSuccessRateImprovementIS) => minSuccessRateImprovementIS <= m.MaxSuccessRateImprovementIS);
        }
    }
}
