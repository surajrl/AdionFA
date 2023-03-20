using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using FluentValidation;

namespace Adion.FA.UI.Station.Module.Dashboard.Validators
{
    public class UploadMarketDataVMValidator : AbstractValidator<UploadMarketDataModel>
    {
        public UploadMarketDataVMValidator()
        {
            RuleFor(model => model.MarketId).NotNull().NotEqual(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, CommonResources.Market));

            RuleFor(model => model.CurrencyPairId).NotNull().NotEqual(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, CommonResources.CurrencyPair));

            RuleFor(model => model.CurrencyPeriodId).NotNull().NotEqual(0)
                .WithMessage(string.Format(ValidationResources.IsRequired, CommonResources.Period));

            RuleFor(model => model.PathFileMarketData).NotEmpty().NotNull().WithName(CommonResources.PathFile);
        }
    }
}
