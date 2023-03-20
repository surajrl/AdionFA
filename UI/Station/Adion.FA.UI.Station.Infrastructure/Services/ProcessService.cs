using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.Contracts.Services;
using Prism.Commands;
using Prism.Ioc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Infrastructure.Services
{
    public class ProcessService : IProcessService
    {
        private ICommand StartProcessProjectCommand { get; set; }
        private ICommand EndAllProcessProjectCommand { get; set; }

        public ProcessService(IApplicationCommands applicationCommands)
        {
            StartProcessProjectCommand = new DelegateCommand<int?>(StartProcessProject, CanStartProcessProject);
            applicationCommands.StartProcessProjectCommand.RegisterCommand(StartProcessProjectCommand);

            EndAllProcessProjectCommand = new DelegateCommand<bool?>(EndAllProcessProject, CanEndAllProcessProject);
            applicationCommands.EndAllProcessProjectCommand.RegisterCommand(EndAllProcessProjectCommand);
        }

        public async void StartProcessProject(int? projectId)
        {
            IProjectServiceAgent service = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            ISharedServiceAgent shareService = ContainerLocator.Container.Resolve<ISharedServiceAgent>();

            ProjectVM project = await service.GetProject(projectId??0);
            EntityServiceHostVM host = await shareService.GetEntityServiceHost((int)EntityTypeEnum.Project, project?.ProjectId ?? 0);

            if (project != null)
            {
                long? currentProjectProcessId = host?.ProcessId;
                List<Process> processes = Process.GetProcessesByName("Adion.FA.UI.Station.Project").ToList();
                if (!processes.Any(p => p.Id == currentProjectProcessId))
                {
                    ProcessStartInfo st = new ProcessStartInfo
                    {
                        FileName = "Adion.FA.UI.Station.Project.exe",
                        Arguments = $"{projectId}_Adion.FA.UI.Station.Project_{project.ProjectName}",
                    };
                    Process process = Process.Start(st);
                    currentProjectProcessId = process.Id;
                }
                await service.UpdateProcessId(projectId.Value, currentProjectProcessId);
            }
        }

        public void StartProcessWekaJava()
        {
            ProcessStartInfo st = new ProcessStartInfo
            {
                FileName = "java.exe",
                Arguments = "-jar Adion.FA.WekaLibrary-0.0.1-SNAPSHOT.jar",
            };
            st.CreateNoWindow = true;
            Process process = Process.Start(st);
        }

        public bool CanStartProcessProject(int? projectId)
        {
            return true;
        }

        public void EndAllProcessProject(bool? includeStation)
        {
            List<Process> processesProject = Process.GetProcessesByName("Adion.FA.UI.Station.Project").ToList();
            List<Process> processesStation = Process.GetProcessesByName("Adion.FA.UI.Station.Project").ToList();

            ((includeStation ?? false) ? processesProject.Union(processesStation) : processesProject).ToList().ForEach(p => 
            {
                p.Kill();
            });
        }

        public bool AnyProcessProject()
        {
            return Process.GetProcessesByName("Adion.FA.UI.Station.Project").ToList().Any();
        }

        public bool CanEndAllProcessProject(bool? includeStation)
        {
            return true;
        }
    }
}
