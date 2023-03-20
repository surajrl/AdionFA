using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace FluentValidationProject
{
    public class EntityModel : BaseModel<EntityModel>, IModelValidator<EntityModel>
    {
        #region Properties

        public int Id { get; set; }

        public string? Firstname { get; set; }

        public IList<EntityModel> Models { get; set; }

        #endregion


        #region Validation

        public ValidationResult? GetValidationResult<TValidator>(Action<ValidationStrategy<EntityModel>>? action = null)
            where TValidator : AbstractValidator<EntityModel>
        {
            var v = Activator.CreateInstance(typeof(TValidator), Array.Empty<object>()) as TValidator;
            ValidationResult? vr = action == null ? v?.Validate(this) : v?.Validate(this, action);
            
            return vr;
        }

        #endregion
    }

}
