using FluentValidation;

using AdionFA.UI.Station.Module.Dashboard.ViewModels;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.UI.Station.Module.Dashboard.Validators
{
    public class DownloadMarketDataVMValidator : AbstractValidator<DownloadHistoricalDataModel>
    {
        public DownloadMarketDataVMValidator()
        {
            RuleFor(model => model.MarketId).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "Market"));

            RuleFor(model => model.SymbolId).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "Symbol"));

            RuleFor(model => model.TimeframeId).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "Timeframe"));

            // TODO: Validate that the start time is at least greater than the year 0004 and before the end date time
            RuleFor(model => model.StartDate).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "Start Date"));

            // TODO: Validate that the end time is less than the current date
            RuleFor(model => model.EndDate).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, "End Date"));
        }
    }
}