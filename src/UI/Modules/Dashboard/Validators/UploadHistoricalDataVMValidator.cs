using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Module.Dashboard.Model;
using FluentValidation;

namespace AdionFA.UI.Station.Module.Dashboard.Validators
{
    public class UploadHistoricalDataVMValidator : AbstractValidator<UploadHistoricalDataModel>
    {
        public UploadHistoricalDataVMValidator()
        {
            RuleFor(model => model.MarketId).NotNull().NotEqual(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, CommonResources.Market));

            RuleFor(model => model.SymbolId).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, CommonResources.Symbols));

            RuleFor(model => model.TimeframeId).NotNull()
                .WithMessage(string.Format(ValidationResources.IsRequired, CommonResources.Timeframe));

            RuleFor(model => model.FilePathHistoricalData).NotEmpty().NotNull().WithName(CommonResources.FilePath);
        }
    }
}
