using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Module.Dashboard.Validators;
using FluentValidation.Results;
using System;

namespace AdionFA.UI.Station.Module.Dashboard.Model
{
    public class ProjectGlobalConfigModel : ProjectGlobalConfigurationVM, IModelValidator
    {
        // America
        public int GlobalScheduleAmericaId { get; set; }

        private DateTime? fromTimeInSecondsAmerica;

        public DateTime? FromTimeInSecondsAmerica
        {
            get => fromTimeInSecondsAmerica;
            set => SetProperty(ref fromTimeInSecondsAmerica, value);
        }

        private DateTime? toTimeInSecondsAmerica;

        public DateTime? ToTimeInSecondsAmerica
        {
            get => toTimeInSecondsAmerica;
            set => SetProperty(ref toTimeInSecondsAmerica, value);
        }

        // Asia
        public int GlobalScheduleAsiaId { get; set; }

        private DateTime? fromTimeInSecondsAsia;

        public DateTime? FromTimeInSecondsAsia
        {
            get => fromTimeInSecondsAsia;
            set => SetProperty(ref fromTimeInSecondsAsia, value);
        }

        private DateTime? toTimeInSecondsAsia;

        public DateTime? ToTimeInSecondsAsia
        {
            get => toTimeInSecondsAsia;
            set => SetProperty(ref toTimeInSecondsAsia, value);
        }

        // Europe
        public int GlobalScheduleEuropeId { get; set; }

        private DateTime? fromTimeInSecondsEurope;

        public DateTime? FromTimeInSecondsEurope
        {
            get => fromTimeInSecondsEurope;
            set => SetProperty(ref fromTimeInSecondsEurope, value);
        }

        private DateTime? toTimeInSecondsEurope;

        public DateTime? ToTimeInSecondsEurope
        {
            get => toTimeInSecondsEurope;
            set => SetProperty(ref toTimeInSecondsEurope, value);
        }

        // Validation
        public ValidationResult GetValidationResult()
        {
            ProjectGlobalConfigVMValidator v = new();
            return v.Validate(this);
        }
    }
}