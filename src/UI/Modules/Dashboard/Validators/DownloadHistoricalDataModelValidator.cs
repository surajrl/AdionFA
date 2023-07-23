using AdionFA.Domain.Properties;
using AdionFA.UI.Module.Dashboard.Model;
using FluentValidation;

namespace AdionFA.UI.Module.Dashboard.Validators
{
    public class DownloadHistoricalDataModelValidator : AbstractValidator<DownloadHistoricalDataModel>
    {
        public DownloadHistoricalDataModelValidator()
        {
            RuleFor(model => model.MarketId).NotNull().GreaterThan(0)
                .WithMessage(string.Format(Resources.NotEmpty, Resources.Market));

            RuleFor(model => model.SymbolId).NotNull().GreaterThan(0)
                .WithMessage(string.Format(Resources.NotEmpty, Resources.Symbol));

            RuleFor(model => model.TimeframeId).NotNull().GreaterThan(0)
                .WithMessage(string.Format(Resources.NotEmpty, Resources.Timeframe));

            RuleFor(model => model.Start).NotNull()
                .WithMessage(string.Format(Resources.NotEmpty, Resources.StartDate));

            RuleFor(model => model.End).NotNull()
                .WithMessage(string.Format(Resources.NotEmpty, Resources.EndDate));
        }
    }
}
