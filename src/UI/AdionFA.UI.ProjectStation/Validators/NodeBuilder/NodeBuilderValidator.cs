using AdionFA.UI.ProjectStation.ViewModels;
using FluentValidation;

namespace AdionFA.UI.ProjectStation.Validators.NodeBuilder
{
    public class NodeBuilderValidator : AbstractValidator<NodeBuilderViewModel>
    {
        public NodeBuilderValidator()
        {
            RuleFor(model => model.NodeBuilderProcesses).NotEmpty()
                .WithMessage("Missing extractor templates");

            RuleFor(model => model.MaxParallelism).GreaterThan(0)
                .WithMessage("Max Parallelism must be greater than 0");
        }
    }
}