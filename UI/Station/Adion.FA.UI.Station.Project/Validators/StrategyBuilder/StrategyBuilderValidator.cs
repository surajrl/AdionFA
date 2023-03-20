using Adion.FA.UI.Station.Project.ViewModels;
using FluentValidation;

namespace Adion.FA.UI.Station.Project.Validators.StrategyBuilder
{
    public class StrategyBuilderValidator : AbstractValidator<StrategyBuilderViewModel>
    {
        public StrategyBuilderValidator()
        {
            RuleFor(model => model.StrategyBuilderProcessList).NotEmpty();
        }
    }
}
