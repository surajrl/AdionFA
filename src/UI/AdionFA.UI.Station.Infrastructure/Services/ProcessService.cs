using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using Prism.Commands;
using Prism.Ioc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Infrastructure.Services
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

        public async void StartProcessProject(int? projectId)
        {
            var service = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            var shareService = ContainerLocator.Container.Resolve<ISharedServiceAgent>();

            var project = await service.GetProjectAsync(projectId ?? 0);

            if (project != null)
            {
                var processes = Process.GetProcessesByName("AdionFA.UI.ProjectStation").ToList();
                var st = new ProcessStartInfo
                {
                    FileName = "AdionFA.UI.ProjectStation.exe",
                    Arguments = $"{projectId}_AdionFA.UI.ProjectStation_{project.ProjectName}",
                };
                var process = Process.Start(st);
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
