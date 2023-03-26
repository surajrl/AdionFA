using FluentValidation.Results;

namespace AdionFA.UI.Station.Infrastructure.Base
{
    public interface IModelValidator
    {
        public ValidationResult GetValidationResult();
    }
}
