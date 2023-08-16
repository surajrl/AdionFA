using AdionFA.Domain.Properties;
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

            RuleFor(m => m.NodeBuilderConfiguration).SetValidator(new NodeBuilderConfigurationBaseVMValidator());

            // Assembly builder

            RuleFor(m => m.AssemblyBuilderConfiguration).SetValidator(new AssemblyBuilderConfigurationBaseVMValidator());

            // Crossing builder

            RuleFor(m => m.CrossingBuilderConfiguration).SetValidator(new CrossingBuilderConfigurationBaseVMValidator());
        }
    }
}
