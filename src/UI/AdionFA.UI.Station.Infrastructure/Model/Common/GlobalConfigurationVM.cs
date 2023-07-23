using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AdionFA.UI.Infrastructure.Model.Common
{
    public class GlobalConfigurationVM : ConfigurationBaseVM, IModelValidator
    {

        public int GlobalConfigurationId { get; set; }

        public IList<GlobalScheduleConfigurationVM> GlobalScheduleConfigurations { get; set; }

        public ValidationResult GetValidationResult()
        {
            GlobalConfigurationVMValidator v = new();
            return v.Validate(this);
        }
    }
}