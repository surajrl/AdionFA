using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure.Contracts.Services;
using Ninject;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdionFA.UI.Infrastructure.Services
{
    public class ProcessService : IProcessService
    {
        private readonly ILogger _logger;
        private Process _wekaProcess;

        public ProcessService(IApplicationCommands applicationCommands)
        {
            _logger = IoC.Kernel.Get<ILogger>();
        }

        public void StartWeka()
        {
            _wekaProcess = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "java.exe",
                    Arguments = "-jar " + Directory.GetCurrentDirectory() + "\\Weka\\AdionFA.WekaLibrary-0.0.1-SNAPSHOT.jar",
                    CreateNoWindow = true
                }
            };

            if (_wekaProcess.Start())
            {
                _logger.Information("ProcessService.StartProcessWeka() :: Weka started.");
            }
        }

        public void EndWeka()
        {
            _wekaProcess?.Kill(true);
        }

        public bool AnyProcessProject()
        {
            return Process.GetProcessesByName("AdionFA.UI.ProjectStation").ToList().Any();
        }
    }
}
