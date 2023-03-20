using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using FluentValidation;

namespace Adion.FA.UI.Station.Module.Dashboard.Validators
{
    class CrateProjectVMValidator : AbstractValidator<CreateProjectModel>
    {
        public CrateProjectVMValidator()
        {
            RuleFor(model => model.ProjectName).NotEmpty();

            RuleFor(model => model.MarketDataId).NotNull().GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
        }
    }
}
