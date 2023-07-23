using AdionFA.UI.Infrastructure.Model.Common;
using FluentValidation;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class GlobalScheduleConfigurationVMValidator : AbstractValidator<GlobalScheduleConfigurationVM>
    {
        public GlobalScheduleConfigurationVMValidator()
        {
            RuleFor(m => m.FromTimeInSeconds).NotNull().NotEmpty();
            RuleFor(m => m.ToTimeInSeconds).NotNull().NotEmpty();
        }
    }
}
