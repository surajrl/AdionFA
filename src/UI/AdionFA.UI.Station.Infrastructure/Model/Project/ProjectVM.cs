using AdionFA.UI.Station.Infrastructure.Model.Base;
using System;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectVM : EntityBaseVM
    {
        public int ProjectId { get; set; }

        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        public IList<ProjectConfigurationVM> ProjectConfigurations { get; set; }

        public DateTime? ProcessLastDate { get; set; }

        public long ProcessId { get; set; }
    }
}
