using AdionFA.Infrastructure.Enums;
using System;

namespace AdionFA.UI.Station.Module.Shell.Model
{
    public class ProjectVM
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        
        public string WorkspacePath { get; set; }
        public string WorkspacePathCut { get; set; }

        public bool IsFavorite { get; set; }

        public int CurrentProjectStepId { get; set; }
        public string CurrentProjectStep => ((ProjectStepEnum)CurrentProjectStepId).GetMetadata()?.Name ?? string.Empty;
        public string ProjectStepIcon => !string.IsNullOrEmpty(CurrentProjectStep) ? $"{CurrentProjectStep.Replace(" ", string.Empty)}Icon" : string.Empty;

        public long? ProcessId { get; set; }
        public DateTime? ProcessLastDate { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime LastLoadOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
