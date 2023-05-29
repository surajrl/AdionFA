using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Project.Model.Configuration;
using FluentValidation;
using System;
using System.Globalization;
using static AdionFA.Infrastructure.Common.Validators.FluentValidator.FluentValidator;

namespace AdionFA.UI.Station.Project.Validators.Configuration
{
    public class ProjectConfigurationSettingVMValidator : AbstractValidator<ProjectSettingsModel>
    {
        public ProjectConfigurationSettingVMValidator()
        {
            // Historical Data

            RuleFor(m => m.HistoricalDataId).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));

            // Configuration Name

            RuleFor(m => m.Description).NotNull().NotEmpty();

            // Extractor

            RuleFor(m => m.ExtractorMinVariation).NotNull().NotEmpty();

            // Period

            RuleFor(m => m.FromDateIS).NotNull().NotEmpty()
                .Must((m, fdis) => fdis < m.ToDateIS && fdis < DateTime.UtcNow).WithMessage(m =>
                        string.Format(CultureInfo.InvariantCulture, ValidationResources.MustBeLessOneAndTwo, ValidationResources.Value, nameof(m.ToDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateIS).NotNull().NotEmpty()
                .Must((m, tdis) => tdis > m.FromDateIS && tdis < DateTime.UtcNow).WithMessage(m =>
                        string.Format(CultureInfo.InvariantCulture, ValidationResources.MustBeGreaterThanOneAndLessThanTow, ValidationResources.Value, nameof(m.FromDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.FromDateOS).NotNull().NotEmpty()
                .Must((m, fdos) => fdos < m.ToDateOS && fdos < DateTime.UtcNow).WithMessage(m =>
                        string.Format(CultureInfo.InvariantCulture, ValidationResources.MustBeLessOneAndTwo, ValidationResources.Value, nameof(m.ToDateOS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateOS).NotNull().NotEmpty()
                .Must((m, tdos) => tdos > m.FromDateOS && tdos < DateTime.UtcNow).WithMessage(m =>
                    string.Format(CultureInfo.InvariantCulture, ValidationResources.MustBeGreaterThanOneAndLessThanTow, ValidationResources.Value, nameof(m.FromDateOS), nameof(DateTime.Today)));

            // Schedules

            When(m => !m.WithoutSchedule, () =>
            {
                RuleFor(m => m.FromTimeInSecondsAmerica).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAmerica).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsAsia).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAsia).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsEurope).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsEurope).NotNull().NotEmpty();
            });

            // Currency

            RuleFor(m => m.SymbolId).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));

            RuleFor(m => m.TimeframeId).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));

            // Weka

            RuleFor(m => m.TotalInstanceWeka).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalDecimalWeka).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.DepthWeka).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinimalSeed).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaximumSeed).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxRatioTree).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.NTotalTree).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            // Strategy Builder

            RuleFor(m => m.SBMinTransactionsIS).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinPercentSuccessIS).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinTransactionsOS).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinPercentSuccessOS).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMaxTransactionsVariation).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.Progressiveness).GreaterThan(0)
                .When(m => m.IsProgressiveness)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMaxPercentCorrelation).GreaterThan(0)
               .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBWinningStrategyUPTarget).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.SBWinningStrategyDOWNTarget).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            // Assembled Builder

            RuleFor(m => m.ABTransactionsTarget).GreaterThan(0)
                 .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.ABMinImprovePercent).GreaterThan(0)
                 .WithMessage(m => string.Format(CultureInfo.InvariantCulture, ValidationResources.NumberGreaterThan, 0));
        }
    }
}
