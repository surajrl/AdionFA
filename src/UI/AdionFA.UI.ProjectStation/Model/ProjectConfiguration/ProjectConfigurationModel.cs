using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Validators.ProjectConfiguration;
using FluentValidation.Results;
using System;

namespace AdionFA.UI.ProjectStation.Model.Configuration
{
    public class ProjectConfigurationModel : ProjectConfigurationVM, IModelValidator
    {
        // America

        public int ProjectScheduleAmericaId { get; set; }

        private DateTime _fromTimeInSecondsAmerica;
        public DateTime FromTimeInSecondsAmerica
        {
            get => _fromTimeInSecondsAmerica;
            set => SetProperty(ref _fromTimeInSecondsAmerica, value);
        }

        private DateTime _toTimeInSecondsAmerica;
        public DateTime ToTimeInSecondsAmerica
        {
            get => _toTimeInSecondsAmerica;
            set => SetProperty(ref _toTimeInSecondsAmerica, value);
        }

        // Asia

        public int ProjectScheduleAsiaId { get; set; }

        private DateTime _fromTimeInSecondsAsia;
        public DateTime FromTimeInSecondsAsia
        {
            get => _fromTimeInSecondsAsia;
            set => SetProperty(ref _fromTimeInSecondsAsia, value);
        }

        private DateTime _toTimeInSecondsAsia;
        public DateTime ToTimeInSecondsAsia
        {
            get => _toTimeInSecondsAsia;
            set => SetProperty(ref _toTimeInSecondsAsia, value);
        }

        // Europe

        public int ProjectScheduleEuropeId { get; set; }

        private DateTime _fromTimeInSecondsEurope;
        public DateTime FromTimeInSecondsEurope
        {
            get => _fromTimeInSecondsEurope;
            set => SetProperty(ref _fromTimeInSecondsEurope, value);
        }

        private DateTime _toTimeInSecondsEurope;
        public DateTime ToTimeInSecondsEurope
        {
            get => _toTimeInSecondsEurope;
            set => SetProperty(ref _toTimeInSecondsEurope, value);
        }

        // Validation
        public ValidationResult GetValidationResult()
        {
            ProjectConfigurationValidator v = new();
            return v.Validate(this);
        }
    }
}
