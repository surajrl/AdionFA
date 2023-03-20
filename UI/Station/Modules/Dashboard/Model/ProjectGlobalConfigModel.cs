using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Module.Dashboard.Validators;
using FluentValidation.Results;
using System;

namespace Adion.FA.UI.Station.Module.Dashboard.Model
{
    public class ProjectGlobalConfigModel : ProjectGlobalConfigurationVM, IModelValidator
    {
        #region Schedule

        //America
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

        //Asia
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

        //Europa
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

        #endregion

        #region Validation

        public ValidationResult GetValidationResult()
        {
            ProjectGlobalConfigVMValidator v = new ProjectGlobalConfigVMValidator();
            return v.Validate(this);
        }

        #endregion
    }
}
