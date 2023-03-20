using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace FluentValidationProject
{
    internal interface IModelValidator<TModel>
    {
        ValidationResult? GetValidationResult<TValidator>(Action<ValidationStrategy<TModel>>? action = null) 
            where TValidator : AbstractValidator<TModel>;
    }
}
