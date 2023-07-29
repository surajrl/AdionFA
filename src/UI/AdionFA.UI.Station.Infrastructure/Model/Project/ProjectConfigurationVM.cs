using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectConfigurationVM : ConfigurationBaseVM, IModelValidator
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectVM Project { get; set; }

        public IList<ProjectScheduleConfigurationVM> ProjectScheduleConfigurations { get; set; }

        // Validation

        public ValidationResult GetValidationResult()
        {
            var v = new ProjectConfigurationVMValidator();
            return v.Validate(this);
        }
    }
}