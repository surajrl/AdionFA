using AdionFA.UI.Station.Project.ViewModels;
using FluentValidation;

namespace AdionFA.UI.Station.Project.Validators.MetaTrader
{
    public class ExpertAdvisorValidator : AbstractValidator<MetaTraderViewModel>
    {
        public ExpertAdvisorValidator()
        {
            RuleFor(model => model.ExpertAdvisor.MagicNumber).NotEmpty()
                .WithMessage("Magic Number is required");

            RuleFor(model => model.ExpertAdvisor.Host).NotEmpty()
                .WithMessage("Host is required");

            RuleFor(model => model.ExpertAdvisor.PublisherPort).NotEmpty()
                .WithMessage("Publisher Port is required");

            RuleFor(model => model.ExpertAdvisor.ResponsePort).NotEmpty()
                .WithMessage("Response Port is required");
        }
    }
}
