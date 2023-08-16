using AdionFA.UI.Infrastructure.Model.Common;
using FluentValidation;

namespace AdionFA.UI.Infrastructure.Validators
{
    public class NodeBuilderConfigurationBaseVMValidator : AbstractValidator<NodeBuilderConfigurationVM>
    {
        public NodeBuilderConfigurationBaseVMValidator()
        {
            RuleFor(m => m.NodesUPTarget)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.NodesDOWNTarget)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.TotalTradesTarget)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.MinTotalTradesIS)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.MinSuccessRatePercentIS)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(m => m.MinTotalTradesOS)
                .GreaterThanOrEqualTo(0);

            RuleFor(m => m.MinSuccessRatePercentOS)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(m => m.MaxSuccessRateVariation)
                .GreaterThanOrEqualTo(0);
        }
    }
}
