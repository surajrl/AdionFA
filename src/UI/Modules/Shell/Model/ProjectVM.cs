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

        public long? ProcessId { get; set; }
        public DateTime? ProcessLastDate { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime LastLoadOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
