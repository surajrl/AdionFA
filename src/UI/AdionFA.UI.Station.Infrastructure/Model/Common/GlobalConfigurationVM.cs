using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Infrastructure.Model.Common
{
    public class GlobalConfigurationVM : ConfigurationBaseVM, IModelValidator
    {
        public int GlobalConfigurationId { get; set; }

        public GlobalConfigurationVM()
        {
            NodeBuilderConfiguration = new();
            AssemblyBuilderConfiguration = new();
            CrossingBuilderConfiguration = new();
        }

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

        public ObservableCollection<GlobalScheduleConfigurationVM> GlobalScheduleConfigurations { get; set; }

        public ValidationResult GetValidationResult()
        {
            var v = new GlobalConfigurationVMValidator();
            return v.Validate(this);
        }
    }
}