using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace Adion.FA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectVM : EntityBaseVM
    {
        #region Properties

        public int ProjectId { get; set; }

        #region ProjectName

        string projectName;
        public string ProjectName
        { 
            get => projectName; 
            set => SetProperty(ref projectName, value); 
        }

        #endregion

        public int ProjectStepId { get; set; }
        public ProjectStepVM ProjectStep { get; set; }

        #endregion

        #region Navegation Properties

        public IList<ProjectConfigurationVM> ProjectConfigurations { get; set; }

        #endregion

        #region Not Mapped

        public DateTime? ProcessLastDate { get; set; }

        public long ProcessId { get; set; }

        #endregion
    }
}
