using FluentValidation.Results;

namespace AdionFA.UI.Infrastructure.Base
{
    public interface IModelValidator
    {
        public ValidationResult GetValidationResult();
    }
}
