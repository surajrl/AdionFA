using AdionFA.UI.Infrastructure.Model.Common;
using FluentValidation;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class GlobalConfigurationVMValidator : AbstractValidator<GlobalConfigurationVM>
    {
        public GlobalConfigurationVMValidator()
        {
            // Schedule

            When(m => !m.WithoutSchedule, () =>
            {
                RuleForEach(m => m.GlobalScheduleConfigurations).SetValidator(new GlobalScheduleConfigurationVMValidator());
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
