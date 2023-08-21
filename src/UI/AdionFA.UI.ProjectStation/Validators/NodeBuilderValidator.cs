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
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.ProjectConfiguration).SetValidator(new ProjectConfigurationVMValidator());
        }
    }
}