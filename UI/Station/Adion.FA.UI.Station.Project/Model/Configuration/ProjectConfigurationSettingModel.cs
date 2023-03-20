using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Project.Validators.Configuration;
using FluentValidation.Results;
using System;

namespace Adion.FA.UI.Station.Project.Model.Configuration
{
    public class ProjectConfigurationSettingModel : ProjectConfigurationVM, IModelValidator
    {
        #region Schedule

        //America
        public int ProjectScheduleAmericaId { get; set; }

        #region FromTimeInSecondsAmerica

        private DateTime fromTimeInSecondsAmerica;
        public DateTime FromTimeInSecondsAmerica
        {
            get { return fromTimeInSecondsAmerica; }
            set { this.SetProperty<DateTime>(ref this.fromTimeInSecondsAmerica, value); }
        }

        #endregion

        #region ToTimeInSecondsAmerica

        private DateTime toTimeInSecondsAmerica;
        public DateTime ToTimeInSecondsAmerica
        {
            get { return toTimeInSecondsAmerica; }
            set { this.SetProperty<DateTime>(ref this.toTimeInSecondsAmerica, value); }
        }

        #endregion

        //Asia
        public int ProjectScheduleAsiaId { get; set; }

        #region FromTimeInSecondsAsia

        private DateTime fromTimeInSecondsAsia;
        public DateTime FromTimeInSecondsAsia
        {
            get { return fromTimeInSecondsAsia; }
            set { this.SetProperty<DateTime>(ref this.fromTimeInSecondsAsia, value); }
        }

        #endregion

        #region ToTimeInSecondsAsia

        private DateTime toTimeInSecondsAsia;
        public DateTime ToTimeInSecondsAsia
        {
            get { return toTimeInSecondsAsia; }
            set { this.SetProperty<DateTime>(ref this.toTimeInSecondsAsia, value); }
        }

        #endregion

        //Europa
        public int ProjectScheduleEuropeId { get; set; }

        #region FromTimeInSecondsEurope

        private DateTime fromTimeInSecondsEurope;
        public DateTime FromTimeInSecondsEurope
        {
            get { return fromTimeInSecondsEurope; }
            set { this.SetProperty<DateTime>(ref this.fromTimeInSecondsEurope, value); }
        }

        #endregion

        #region ToTimeInSecondsEurope

        private DateTime toTimeInSecondsEurope;
        public DateTime ToTimeInSecondsEurope
        {
            get { return toTimeInSecondsEurope; }
            set { this.SetProperty<DateTime>(ref this.toTimeInSecondsEurope, value); }
        }

        #endregion

        #endregion

        #region Validation

        public ValidationResult GetValidationResult()
        {
            ProjectConfigurationSettingVMValidator v = new ProjectConfigurationSettingVMValidator();
            return v.Validate(this);
        }

        #endregion
    }
}
