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
        private ICommand StartProcessProjectCommand { get; set; }
        private ICommand EndAllProcessProjectCommand { get; set; }

        public ProcessService(IApplicationCommands applicationCommands)
        {
            StartProcessProjectCommand = new DelegateCommand<int?>(StartProcessProject);
            applicationCommands.StartProcessProjectCommand.RegisterCommand(StartProcessProjectCommand);

            EndAllProcessProjectCommand = new DelegateCommand<bool?>(EndAllProcessProject);
            applicationCommands.EndAllProcessProjectCommand.RegisterCommand(EndAllProcessProjectCommand);
        }

        public void StartProcessProject(int? projectId)
        {
            var project = IoC.Kernel.Get<IProjectAppService>().GetProject(projectId ?? 0);

            if (project != null)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "AdionFA.UI.ProjectStation.exe",
                    Arguments = $"{projectId}_AdionFA.UI.ProjectStation_{project.ProjectName}",
                });
            }
        }

        public void StartProcessWekaJava()
        {
            var wekaProcessStartInfo = new ProcessStartInfo()
            {
                FileName = "java.exe",
                Arguments = "-jar " + Directory.GetCurrentDirectory() + "\\Weka\\AdionFA.WekaLibrary-0.0.1-SNAPSHOT.jar",
                CreateNoWindow = true
            };

            Process.Start(wekaProcessStartInfo);
        }

        public void EndAllProcessProject(bool? includeStation)
        {
            var processesProject = Process.GetProcessesByName("AdionFA.UI.ProjectStation").ToList();
            var processesStation = Process.GetProcessesByName("AdionFA.UI.Station").ToList();

            ((includeStation ?? false) ? processesProject.Union(processesStation) : processesProject).ToList().ForEach(p =>
            {
                p.Kill();
            });
        }

        public bool AnyProcessProject()
        {
            return Process.GetProcessesByName("AdionFA.UI.ProjectStation").ToList().Any();
        }
    }
}
