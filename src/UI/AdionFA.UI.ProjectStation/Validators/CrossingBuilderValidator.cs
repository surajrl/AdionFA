using AdionFA.UI.Infrastructure.Validators;
using AdionFA.UI.ProjectStation.ViewModels;
using FluentValidation;

namespace AdionFA.UI.ProjectStation.Validators
{
    public class CrossingBuilderValidator : AbstractValidator<CrossingBuilderViewModel>
    {
        public CrossingBuilderValidator()
        {
            RuleFor(model => model.CrossingBuilderProcessesUP)
                .NotEmpty()
                .When(model => model.CrossingBuilderProcessesDOWN.Count == 0)
                .WithMessage("No nodes to build");

            RuleFor(model => model.CrossingBuilderProcessesDOWN)
                .NotEmpty()
                .When(model => model.CrossingBuilderProcessesUP.Count == 0)
                .WithMessage("No nodes to build");

            RuleFor(model => model.CrossingHistoricalDataId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Choose a crossing historical data");

            RuleFor(model => model.ProjectConfiguration)
                .SetValidator(new ProjectConfigurationVMValidator());
        }
    }
}
