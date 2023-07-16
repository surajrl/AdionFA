using AdionFA.Domain.Properties;
using AdionFA.UI.Module.Dashboard.Model;
using FluentValidation;

namespace AdionFA.UI.Module.Dashboard.Validators
{
    class CreateProjectVMValidator : AbstractValidator<CreateProjectModel>
    {
        public CreateProjectVMValidator()
        {
            RuleFor(model => model.ProjectName).NotEmpty();

            RuleFor(model => model.HistoricalDataId).NotNull().GreaterThan(0)
                .WithMessage(m => string.Format(Resources.NumberGreaterThan, 0));
        }
    }
}
