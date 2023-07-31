using AdionFA.Domain.Properties;
using AdionFA.UI.Module.Dashboard.Model;
using FluentValidation;

namespace AdionFA.UI.Module.Dashboard.Validators
{
    public class UploadHistoricalDataModelValidator : AbstractValidator<UploadHistoricalDataModel>
    {
        public UploadHistoricalDataModelValidator()
        {
            RuleFor(model => model.MarketId).NotNull().NotEqual(0)
                .WithMessage(string.Format(Resources.NotEmpty, Resources.Market));

            RuleFor(model => model.SymbolId).NotNull()
                .WithMessage(string.Format(Resources.NotEmpty, Resources.Symbols));

            RuleFor(model => model.TimeframeId).NotNull()
                .WithMessage(string.Format(Resources.NotEmpty, Resources.Timeframe));

            RuleFor(model => model.FilePathHistoricalData).NotEmpty().NotNull().WithName(Resources.Filepath);
        }
    }
}
