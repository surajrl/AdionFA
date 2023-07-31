using AdionFA.UI.Infrastructure.Model.Project;
using FluentValidation;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class ProjectVMValidator : AbstractValidator<ProjectVM>
    {
        public ProjectVMValidator()
        {
            RuleFor(m => m.ProjectName).NotNull().NotEmpty()
                .WithMessage(m => "Enter a Project Name");

            RuleFor(m => m.HistoricalDataId).GreaterThan(0)
                .WithMessage(m => "Select Historical Data");
        }
    }
}
