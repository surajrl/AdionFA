using AdionFA.UI.Station.Project.ViewModels;
using FluentValidation;

namespace AdionFA.UI.Station.Project.Validators.Extractor
{
    public class ExtractorValidator : AbstractValidator<ExtractorViewModel>
    {
        public ExtractorValidator()
        {
            RuleFor(model => model.StartDate).NotNull();

            RuleFor(model => model.EndDate).NotNull();

            RuleFor(model => model.ExtractionProcessList).NotEmpty();
        }
    }
}
