using System.Reactive.Concurrency;

namespace AdionFA.UI.Station.Infrastructure.Contracts.Services
{
    public interface ISchedulerProvider
    {
        IScheduler MainThread { get; }
        IScheduler Background { get; }
    }
}
