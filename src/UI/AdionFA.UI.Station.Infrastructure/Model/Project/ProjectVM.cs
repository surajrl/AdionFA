using AdionFA.TransferObject.MarketData;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectVM : EntityBaseVM, IModelValidator
    {
        public int ProjectId { get; set; }

        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        private string _workspacePath;
        public string WorkspacePath
        {
            get => _workspacePath;
            set => SetProperty(ref _workspacePath, value);
        }

        public HistoricalDataDTO HistoricalData { get; set; }
        private int _historicalDataId;
        public int HistoricalDataId
        {
            get => _historicalDataId;
            set => SetProperty(ref _historicalDataId, value);
        }

        public ProjectConfigurationVM ProjectConfiguration { get; set; }
        private int _projectConfigurationId;
        public int ProjectConfigurationId
        {
            get => _projectConfigurationId;
            set => SetProperty(ref _projectConfigurationId, value);
        }

        // Validation

        public ValidationResult GetValidationResult()
        {
            var v = new ProjectVMValidator();
            return v.Validate(this);
        }
    }
}
