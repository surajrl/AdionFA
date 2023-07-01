using AdionFA.UI.Station.Project.ViewModels;
using FluentValidation;

namespace AdionFA.UI.Station.Project.Validators.CrossingBuilder
{
    public class CrossingBuilderValidator : AbstractValidator<CrossingBuilderViewModel>
    {
        public CrossingBuilderValidator()
        {
            RuleFor(model => model.CrossingBuilderProcessesUP).NotEmpty()
                .WithMessage("Missing extractor eemplates");
            RuleFor(model => model.CrossingBuilderProcessesDOWN).NotEmpty()
                .WithMessage("Missing extractor templates");
            RuleFor(model => model.CrossingHistoricalDataId).GreaterThanOrEqualTo(1)
                .WithMessage("Choose a crossing historical data");
        }
    }
}
