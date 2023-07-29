using AdionFA.Application.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure.Contracts.Services;
using Ninject;
using Prism.Commands;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Infrastructure.Services
{
    public class ProcessService : IProcessService
    {
        public ProcessService(IApplicationCommands applicationCommands)
        {
            StartProcessProjectCommand = new DelegateCommand<int?>(StartProcessProject);

            applicationCommands.StartProcessProjectCommand.RegisterCommand(StartProcessProjectCommand);
        }

        public ICommand StartProcessProjectCommand { get; set; }
        public void StartProcessProject(int? projectId)
        {
            var project = IoC.Kernel.Get<IProjectService>().GetProject(projectId ?? 0, false);

            Process.Start(new ProcessStartInfo
            {
                FileName = "AdionFA.UI.ProjectStation.exe",
                Arguments = $"{projectId}_AdionFA.UI.ProjectStation_{project.ProjectName}",
            });
        }

        public void StartProcessWeka()
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "java.exe",
                Arguments = "-jar " + Directory.GetCurrentDirectory() + "\\Weka\\AdionFA.WekaLibrary-0.0.1-SNAPSHOT.jar",
                CreateNoWindow = true
            });
        }

        public bool AnyProcessProject()
        {
            return Process.GetProcessesByName("AdionFA.UI.ProjectStation").ToList().Any();
        }
    }
}
