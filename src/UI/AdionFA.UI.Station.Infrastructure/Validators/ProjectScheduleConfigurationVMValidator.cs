using AdionFA.UI.Infrastructure.Model.Project;
using FluentValidation;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class ProjectScheduleConfigurationVMValidator : AbstractValidator<ProjectScheduleConfigurationVM>
    {
        public ProjectScheduleConfigurationVMValidator()
        {
            RuleFor(m => m.FromTimeInSeconds)
                .NotNull()
                .NotEmpty();

            RuleFor(m => m.ToTimeInSeconds)
                .NotNull()
                .NotEmpty();
        }
    }
}
