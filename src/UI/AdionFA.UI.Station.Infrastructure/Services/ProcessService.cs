using AdionFA.Application.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure.Contracts.Services;
using Ninject;
using Prism.Commands;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Infrastructure.Services
{
    public class ProcessService : IProcessService
    {
        private readonly ILogger _logger;

        public ProcessService(IApplicationCommands applicationCommands)
        {
            _logger = IoC.Kernel.Get<ILogger>();

            StartProcessProjectCommand = new DelegateCommand<int?>(StartProcessProject);

            applicationCommands.StartProcessProjectCommand.RegisterCommand(StartProcessProjectCommand);
        }

        public ICommand StartProcessProjectCommand { get; set; }
        public void StartProcessProject(int? projectId)
        {
            var project = IoC.Kernel.Get<IProjectService>().GetProject(projectId ?? 0, false);

            var process = new Process
            {
                StartInfo = new()
                {
                    FileName = "AdionFA.UI.ProjectStation.exe",
                    Arguments = $"{projectId}_AdionFA.UI.ProjectStation_{project.ProjectName}",
                }
            };

            if (process.Start())
            {
                _logger.Information($"ProcessService.StartProcessProject() :: Project {projectId} started.");
            }
            else
            {
                _logger.Error($"ProcessService.StartProcessProject() :: Project {projectId} not started.");

            }
        }

        public void StartProcessWeka()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "java.exe",
                    Arguments = "-jar " + Directory.GetCurrentDirectory() + "\\Weka\\AdionFA.WekaLibrary-0.0.1-SNAPSHOT.jar",
                    CreateNoWindow = true
                }
            };

            if (process.Start())
            {
                _logger.Information("ProcessService.StartProcessWeka() :: Weka started.");
            }
            else
            {
                _logger.Error("ProcessService.StartProcessWeka() :: Weka not started.");
            }
        }

        public bool AnyProcessProject()
        {
            return Process.GetProcessesByName("AdionFA.UI.ProjectStation").ToList().Any();
        }
    }
}
