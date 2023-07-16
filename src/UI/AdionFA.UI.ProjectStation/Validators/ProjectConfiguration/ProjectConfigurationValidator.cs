using AdionFA.Domain.Properties;
using AdionFA.UI.ProjectStation.Model.Configuration;
using FluentValidation;
using System;
using System.Globalization;
using static AdionFA.Infrastructure.Validators.FluentValidator.FluentValidator;

namespace AdionFA.UI.ProjectStation.Validators.ProjectConfiguration
{
    public class ProjectConfigurationValidator : AbstractValidator<ProjectConfigurationModel>
    {
        public ProjectConfigurationValidator()
        {
            // Historical Data

            RuleFor(m => m.HistoricalDataId).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NotNull, FluentPlaceholders.PropertyName));

            // Configuration Name

            RuleFor(m => m.Description).NotNull().NotEmpty();

            // Extractor

            RuleFor(m => m.ExtractorMinVariation).NotNull().NotEmpty();

            // Period

            RuleFor(m => m.FromDateIS).NotNull().NotEmpty()
                .Must((m, fdis) => fdis < m.ToDateIS && fdis < DateTime.UtcNow).WithMessage(m =>
                        string.Format(CultureInfo.InvariantCulture, Resources.MustBeLessOneAndTwo, Resources.Value, nameof(m.ToDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateIS).NotNull().NotEmpty()
                .Must((m, tdis) => tdis > m.FromDateIS && tdis < DateTime.UtcNow).WithMessage(m =>
                        string.Format(CultureInfo.InvariantCulture, Resources.MustBeGreaterThanOneAndLessThanTow, Resources.Value, nameof(m.FromDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.FromDateOS).NotNull().NotEmpty()
                .Must((m, fdos) => fdos < m.ToDateOS && fdos < DateTime.UtcNow).WithMessage(m =>
                        string.Format(CultureInfo.InvariantCulture, Resources.MustBeLessOneAndTwo, Resources.Value, nameof(m.ToDateOS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateOS).NotNull().NotEmpty()
                .Must((m, tdos) => tdos > m.FromDateOS && tdos < DateTime.UtcNow).WithMessage(m =>
                    string.Format(CultureInfo.InvariantCulture, Resources.MustBeGreaterThanOneAndLessThanTow, Resources.Value, nameof(m.FromDateOS), nameof(DateTime.Today)));

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
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NotNull, FluentPlaceholders.PropertyName));

            RuleFor(m => m.TimeframeId).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NotNull, FluentPlaceholders.PropertyName));

            // Weka

            RuleFor(m => m.TotalInstanceWeka).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalDecimalWeka).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.DepthWeka).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.MinimalSeed).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.MaximumSeed).GreaterThan(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxRatioTree).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.NTotalTree).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            // Strategy Builder

            RuleFor(m => m.SBMinTransactionsIS).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinSuccessRatePercentIS).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinTransactionsOS).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMinSuccessRatePercentOS).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMaxSuccessRateVariation).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxProgressivenessVariation).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
                .When(m => m.IsProgressiveness)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBMaxCorrelationPercent).GreaterThanOrEqualTo(0)
               .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBWinningStrategyUPTarget).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.SBWinningStrategyDOWNTarget).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            // Assembly Builder

            RuleFor(m => m.ABTransactionsTarget).GreaterThanOrEqualTo(0)
                 .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));

            RuleFor(m => m.ABMinImprovePercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
                 .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.NumberGreaterThan, 0));
        }
    }
}
