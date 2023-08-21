using AdionFA.UI.Infrastructure.Validators;
using AdionFA.UI.ProjectStation.ViewModels;
using FluentValidation;

namespace AdionFA.UI.ProjectStation.Validators
{
    public class AssemblyBuilderValidator : AbstractValidator<AssemblyBuilderViewModel>
    {
        public AssemblyBuilderValidator()
        {
            RuleFor(model => model.AssemblyBuilderProcessesDOWN)
                .NotEmpty()
                .When(model => model.AssemblyBuilderProcessesUP.Count == 0);

            RuleFor(model => model.AssemblyBuilderProcessesUP)
                .NotEmpty()
                .When(model => model.AssemblyBuilderProcessesDOWN.Count == 0);

            RuleFor(model => model.ProjectConfiguration).SetValidator(new ProjectConfigurationVMValidator());
        }
    }
}
