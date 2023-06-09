﻿using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Module.Dashboard.Model;
using FluentValidation;

namespace AdionFA.UI.Station.Module.Dashboard.Validators
{
    class CreateProjectVMValidator : AbstractValidator<CreateProjectModel>
    {
        public CreateProjectVMValidator()
        {
            RuleFor(model => model.ProjectName).NotEmpty();

            RuleFor(model => model.HistoricalDataId).NotNull().GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
        }
    }
}
