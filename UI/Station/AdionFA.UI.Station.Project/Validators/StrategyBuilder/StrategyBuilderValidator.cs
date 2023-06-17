using AdionFA.UI.Station.Project.ViewModels;
using FluentValidation;

namespace AdionFA.UI.Station.Project.Validators.StrategyBuilder
{
    public class StrategyBuilderValidator : AbstractValidator<StrategyBuilderViewModel>
    {
        public StrategyBuilderValidator()
        {
            RuleFor(model => model.StrategyBuilderProcesses).NotEmpty();
            RuleFor(model => model.MaxParallelism).GreaterThan(0);
        }
    }
}