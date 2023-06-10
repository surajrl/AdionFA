using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Module.Dashboard.Model;
using FluentValidation;

namespace AdionFA.UI.Station.Module.Dashboard.Validators
{
    public class ProjectGlobalConfigVMValidator : AbstractValidator<ConfigurationModel>
    {
        public ProjectGlobalConfigVMValidator()
        {
            When(m => !m.WithoutSchedule, () =>
            {
                RuleFor(m => m.FromTimeInSecondsAmerica).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAmerica).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsAsia).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAsia).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsEurope).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsEurope).NotNull().NotEmpty();
            });

            RuleFor(m => m.SymbolId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.IsRequired, CommonResources.Symbols));

            RuleFor(m => m.TimeframeId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.IsRequired, CommonResources.Timeframe));

            RuleFor(m => m.TotalInstanceWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalDecimalWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.DepthWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinimalSeed).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaximumSeed).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxRatioTree).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.NTotalTree).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));


            RuleFor(m => m.SBMinTransactionsIS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinSuccessRatePercentIS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinTransactionsOS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinSuccessRatePercentOS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMaxSuccessRateVariation).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxProgressivenessVariation).GreaterThan(0)
                .When(m => m.IsProgressiveness)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMaxCorrelationPercent).GreaterThan(0)
               .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBWinningStrategyUPTarget).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBWinningStrategyDOWNTarget).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.ABTransactionsTarget).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.ABMinImprovePercent).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
        }
    }
}