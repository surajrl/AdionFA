using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Module.Dashboard.Validators;
using FluentValidation.Results;

namespace Adion.FA.UI.Station.Module.Dashboard.Model
{
    public class CreateProjectModel : ProjectVM, IModelValidator
    {
        #region Properties

        int? configurationId;
        public int? ConfigurationId
        {
            get => configurationId;
            set => SetProperty(ref configurationId, value);
        }

        int? marketDataId;
        public int? MarketDataId
        {
            get => marketDataId;
            set => SetProperty(ref marketDataId, value);
        }

        #endregion

        #region Validation

        public ValidationResult GetValidationResult()
        {
            CrateProjectVMValidator v = new CrateProjectVMValidator();
            return v.Validate(this);
        }

        #endregion
    }
}
