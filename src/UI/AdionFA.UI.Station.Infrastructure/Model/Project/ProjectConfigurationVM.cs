using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.Common;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectConfigurationVM : ConfigurationBaseVM, IModelValidator
    {
        public int ProjectConfigurationId { get; set; }

        // Project

        public int ProjectId { get; set; }
        public ProjectVM Project { get; set; }

        // Builder configuration

        private NodeBuilderConfigurationVM _nodeBuilderConfiguration;
        public NodeBuilderConfigurationVM NodeBuilderConfiguration
        {
            get => _nodeBuilderConfiguration;
            set => SetProperty(ref _nodeBuilderConfiguration, value);
        }

        private AssemblyBuilderConfigurationVM _assemblyBuilderConfiguration;
        public AssemblyBuilderConfigurationVM AssemblyBuilderConfiguration
        {
            get => _assemblyBuilderConfiguration;
            set => SetProperty(ref _assemblyBuilderConfiguration, value);
        }

        private CrossingBuilderConfigurationVM _crossingBuilderConfiguration;
        public CrossingBuilderConfigurationVM CrossingBuilderConfiguration
        {
            get => _crossingBuilderConfiguration;
            set => SetProperty(ref _crossingBuilderConfiguration, value);
        }

        // Navigation

        public ObservableCollection<ProjectScheduleConfigurationVM> ProjectScheduleConfigurations { get; set; }

        // Validation

        public ValidationResult GetValidationResult()
        {
            var v = new ProjectConfigurationVMValidator();
            return v.Validate(this);
        }
    }
}