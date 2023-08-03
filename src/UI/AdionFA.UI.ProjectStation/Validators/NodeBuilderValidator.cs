using AdionFA.UI.Infrastructure.Validators;
using AdionFA.UI.ProjectStation.ViewModels;
using FluentValidation;

namespace AdionFA.UI.ProjectStation.Validators
{
    public class NodeBuilderValidator : AbstractValidator<NodeBuilderViewModel>
    {
        public NodeBuilderValidator()
        {
            RuleFor(model => model.NodeBuilderProcesses)
                .NotEmpty()
                .WithMessage("Add extractor templates");

            RuleFor(model => model.MaxParallelism)
                .GreaterThan(0);

            RuleFor(model => model.ProjectConfiguration)
                .SetValidator(new ProjectConfigurationVMValidator());
        }
    }
}