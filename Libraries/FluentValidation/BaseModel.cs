using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace FluentValidationProject
{
    public abstract class BaseModel<TModel> 
    {
        public ValidationResult? Validate<TValidator>(Action<ValidationStrategy<TModel>>? action = null) where TValidator : AbstractValidator<TModel>
        {
            ValidationResult? vr = ((IModelValidator<TModel>)this).GetValidationResult<TValidator>(action);
            return vr;
        }
    }
}
