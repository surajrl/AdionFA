using FluentValidation;
using FluentValidationProject.Resources;
using static FluentValidator;

namespace FluentValidationProject
{
    internal class EntityModelValidator : AbstractValidator<EntityModel>
    {
        public EntityModelValidator()
        {
            RuleSet("Names", () => 
            {
                RuleFor(m => m.Firstname).NotNull();
            });

            //RuleFor(m => m.Id).NotEqual(0);

            RuleFor(m => m.Firstname).NotNull().WithName("Name");

            //RuleFor(model => model.Models).NotEmpty().NotNull();

            RuleForEach(m => m.Models).NotNull().WithMessage(FluentPlaceholders.CollectionIndex);

            //RuleForEach(m => m.Models).Must(m => m.Count)
        }
    }
}
