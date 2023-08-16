using AdionFA.Domain.Properties;
using AdionFA.UI.Infrastructure.Model.Project;
using FluentValidation;
using System;
using System.Globalization;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class ProjectConfigurationVMValidator : AbstractValidator<ProjectConfigurationVM>
    {
        public ProjectConfigurationVMValidator()
        {
            // Schedule

            When(m => !m.WithoutSchedule, () =>
            {
                RuleForEach(m => m.ProjectScheduleConfigurations).SetValidator(new ProjectScheduleConfigurationVMValidator());
            });

            // Period

            RuleFor(m => m.FromDateIS)
                .NotNull()
                .NotEmpty()
                .Must((m, fromDateIS) => fromDateIS < m.ToDateIS && fromDateIS < DateTime.UtcNow)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.MustBeLessOneAndTwo, Resources.Value, nameof(m.ToDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateIS)
                .NotNull()
                .NotEmpty()
                .Must((m, toDateIS) => toDateIS > m.FromDateIS && toDateIS < DateTime.UtcNow)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.MustBeGreaterThanOneAndLessThanTow, Resources.Value, nameof(m.FromDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.FromDateOS)
                .NotNull()
                .NotEmpty()
                .Must((m, fromDateOS) => fromDateOS < m.ToDateOS && fromDateOS < DateTime.UtcNow)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.MustBeLessOneAndTwo, Resources.Value, nameof(m.ToDateOS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateOS)
                .NotNull()
                .NotEmpty()
                .Must((m, toDateOS) => toDateOS > m.FromDateOS && toDateOS < DateTime.UtcNow)
                .WithMessage(m => string.Format(CultureInfo.InvariantCulture, Resources.MustBeGreaterThanOneAndLessThanTow, Resources.Value, nameof(m.FromDateOS), nameof(DateTime.Today)));

            // Extractor

            RuleFor(m => m.ExtractorMinVariation).GreaterThanOrEqualTo(0)
                .WithMessage(m => string.Format(Resources.NumberGreaterThanOrEqualTo, 0));

            // MetaTrader

            RuleFor(m => m.ExpertAdvisorHost).NotNull().NotEmpty();
            RuleFor(m => m.ExpertAdvisorPublisherPort).NotNull().NotEmpty();
            RuleFor(m => m.ExpertAdvisorResponsePort).NotNull().NotEmpty();

            // Weka

            RuleFor(m => m.TotalDecimalWeka).GreaterThan(0)
                .WithMessage(m => string.Format(Resources.NumberGreaterThan, 0));

            RuleFor(m => m.MinimalSeed).GreaterThan(0)
                .WithMessage(m => string.Format(Resources.NumberGreaterThan, 0));

            RuleFor(m => m.MaximumSeed).GreaterThan(0)
                .WithMessage(m => string.Format(Resources.NumberGreaterThan, 0));

            // Node builder

            // Assembly builder

            // Crossing builder
        }
    }
}
