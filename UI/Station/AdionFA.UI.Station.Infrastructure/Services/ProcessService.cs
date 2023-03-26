using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using Prism.Commands;
using Prism.Ioc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.IO;

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
            IProjectServiceAgent service = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            ISharedServiceAgent shareService = ContainerLocator.Container.Resolve<ISharedServiceAgent>();

            ProjectVM project = await service.GetProject(projectId ?? 0);
            EntityServiceHostVM host = await shareService.GetEntityServiceHost((int)EntityTypeEnum.Project, project?.ProjectId ?? 0);

            if (project != null)
            {
                long? currentProjectProcessId = host?.ProcessId;
                List<Process> processes = Process.GetProcessesByName("AdionFA.UI.Station.Project").ToList();
                if (!processes.Any(p => p.Id == currentProjectProcessId))
                {
                    ProcessStartInfo st = new ProcessStartInfo
                    {
                        FileName = "AdionFA.UI.Station.Project.exe",
                        Arguments = $"{projectId}_AdionFA.UI.Station.Project_{project.ProjectName}",
                    };
                    Process process = Process.Start(st);
                    currentProjectProcessId = process.Id;
                }
                await service.UpdateProcessId(projectId.Value, currentProjectProcessId);
            }
        }

        public void StartProcessWekaJava()
        {
            ProcessStartInfo st = new()
            {
                FileName = "java.exe",
                Arguments = "-jar " + Directory.GetCurrentDirectory() + "\\Weka\\AdionFA.WekaLibrary-0.0.1-SNAPSHOT.jar",
                CreateNoWindow = true
            };
            Process.Start(st);
        }

        public void StartProcessMetaTrader()
        {
            ProcessStartInfo metaTraderProcess = new()
            {
                FileName = """C:\\Program Files\\MetaTrader 5\\terminal64.exe""",
                Arguments = "/config:" + Directory.GetCurrentDirectory() + "\\MetaTrader\\config.ini",
                CreateNoWindow = true
            };

            Process.Start(metaTraderProcess);
        }

        public void EndAllProcessProject(bool? includeStation)
        {
            List<Process> processesProject = Process.GetProcessesByName("AdionFA.UI.Station.Project").ToList();
            List<Process> processesStation = Process.GetProcessesByName("AdionFA.UI.Station").ToList();

            ((includeStation ?? false) ? processesProject.Union(processesStation) : processesProject).ToList().ForEach(p =>
            {
                p.Kill();
            });
        }

        public bool AnyProcessProject()
        {
            return Process.GetProcessesByName("AdionFA.UI.Station.Project").ToList().Any();
        }
    }
}
