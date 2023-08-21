using AdionFA.UI.Infrastructure.Model.Project;
using FluentValidation;
using System;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class ProjectConfigurationVMValidator : AbstractValidator<ProjectConfigurationVM>
    {
        public ProjectConfigurationVMValidator()
        {

            // Period

            RuleFor(m => m.FromDateIS)
                .NotNull()
                .NotEmpty()
                .Must((m, fromDateIS) => fromDateIS < m.ToDateIS && fromDateIS < DateTime.UtcNow);

            RuleFor(m => m.ToDateIS)
                .NotNull()
                .NotEmpty()
                .Must((m, toDateIS) => toDateIS > m.FromDateIS && toDateIS < DateTime.UtcNow);

            RuleFor(m => m.FromDateOS)
                .NotNull()
                .NotEmpty()
                .Must((m, fromDateOS) => fromDateOS < m.ToDateOS && fromDateOS < DateTime.UtcNow);

            RuleFor(m => m.ToDateOS)
                .NotNull()
                .NotEmpty()
                .Must((m, toDateOS) => toDateOS > m.FromDateOS && toDateOS < DateTime.UtcNow);

            // Schedule

            When(m => !m.WithoutSchedule, () =>
            {
                RuleForEach(m => m.ProjectScheduleConfigurations).SetValidator(new ProjectScheduleConfigurationVMValidator());
            });

            // Extractor

            RuleFor(m => m.ExtractorMinVariation)
                .GreaterThanOrEqualTo(0);

            // MetaTrader

            RuleFor(m => m.ExpertAdvisorHost)
                .NotNull()
                .NotEmpty();

            RuleFor(m => m.ExpertAdvisorResponsePort)
                .NotNull()
                .NotEmpty();

            RuleFor(m => m.ExpertAdvisorPublisherPort)
                .NotNull()
                .NotEmpty();

            // Weka

            RuleFor(m => m.MinimalSeed)
                .GreaterThan(0);

            RuleFor(m => m.MaximumSeed)
                .GreaterThan(0);

            RuleFor(m => m.TotalDecimalWeka)
                .GreaterThan(0);

            // Builder

            RuleFor(m => m.MaxCorrelationPercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            When(m => m.IsProgressiveness, () =>
            {
                RuleFor(m => m.MaxProgressivenessVariation)
                .GreaterThan(0);
            });

            // Node builder

            RuleFor(m => m.NodeBuilderConfiguration).SetValidator(new NodeBuilderConfigurationBaseVMValidator());

            // Assembly builder

            RuleFor(m => m.AssemblyBuilderConfiguration).SetValidator(new AssemblyBuilderConfigurationBaseVMValidator());

            // Crossing builder

            RuleFor(m => m.CrossingBuilderConfiguration).SetValidator(new CrossingBuilderConfigurationBaseVMValidator());
        }
    }
}
