using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using FluentValidation;

namespace Adion.FA.UI.Station.Module.Dashboard.Validators
{
    public class ProjectGlobalConfigVMValidator : AbstractValidator<ProjectGlobalConfigModel>
    {
        public ProjectGlobalConfigVMValidator()
        {
            #region Schedules

            When(m => !m.WithoutSchedule, () =>
            {
                RuleFor(m => m.FromTimeInSecondsAmerica).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAmerica).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsAsia).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAsia).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsEurope).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsEurope).NotNull().NotEmpty();
            });

            #endregion

            #region Currency

            RuleFor(m => m.CurrencyPairId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.IsRequired, CommonResources.CurrencyPair));

            RuleFor(m => m.CurrencyPeriodId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.IsRequired, CommonResources.Period));

            RuleFor(m => m.CurrencySpreadId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.IsRequired, CommonResources.Spread));

            #endregion

            #region Weka

            RuleFor(m => m.TotalInstanceWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalDecimalWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.DepthWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustDepthWeka).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinimalSeed).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaximumSeed).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxRatioTree).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMaxRatioTree).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.NTotalTree).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustNTotalTree).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            #endregion

            #region Strategy Builder

            RuleFor(m => m.MinTransactionCountIS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinTransactionCountIS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinPercentSuccessIS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinPercentSuccessIS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));


            RuleFor(m => m.MinTransactionCountOS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinTransactionCountOS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinPercentSuccessOS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinPercentSuccessOS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.VariationTransaction).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustVariationTransaction).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.Progressiveness).GreaterThan(0)
                .When(m => m.IsProgressiveness)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustProgressiveness).GreaterThan(0)
                .When(m => m.IsProgressiveness && m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxPercentCorrelation).GreaterThan(0)
               .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.WinningStrategyTotalUP).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.WinningStrategyTotalDOWN).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxAdjustConfig).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            #endregion

            #region Assembled Builder

            RuleFor(m => m.TransactionTarget).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinAssemblyPercent).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalAssemblyIterations).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            #endregion
        }
    }
}
