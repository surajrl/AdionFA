/*using AdionFA.UI.Station.Modules.Infrastructure.Contracts.Services;
using System.Reactive.Concurrency;
using System.Windows.Threading;

namespace AdionFA.UI.Station.Modules.Infrastructure.Services
{
    public class SchedulerProvider : ISchedulerProvider
    {
        public SchedulerProvider(Dispatcher dispatcher)
        {
            MainThread = new DispatcherScheduler(dispatcher);
        }

        public IScheduler MainThread { get; }
        public IScheduler Background => TaskPoolScheduler.Default;
    }
}
*/