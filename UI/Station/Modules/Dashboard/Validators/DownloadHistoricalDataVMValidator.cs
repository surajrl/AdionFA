using FluentValidation;

using AdionFA.UI.Station.Module.Dashboard.ViewModels;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.UI.Station.Module.Dashboard.Validators
{
    public class DownloadHistoricalDataVMValidator : AbstractValidator<DownloadHistoricalDataModel>
    {
        public DownloadHistoricalDataVMValidator()
        {
            RuleFor(model => model.MarketId).NotNull().GreaterThan(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, "Market"));

            RuleFor(model => model.SymbolId).NotNull().GreaterThan(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, "Symbol"));

            RuleFor(model => model.TimeframeId).NotNull().GreaterThan(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, "Timeframe"));

            RuleFor(model => model.Start).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "Start Date"));

            RuleFor(model => model.End).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "End Date"));
        }
    }
}
