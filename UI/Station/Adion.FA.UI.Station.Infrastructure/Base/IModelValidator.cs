using FluentValidation.Results;

namespace Adion.FA.UI.Station.Infrastructure.Base
{
    public interface IModelValidator
    {
        public ValidationResult GetValidationResult();
    }
}
