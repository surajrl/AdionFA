using System.Reactive.Concurrency;

namespace Adion.FA.UI.Station.Infrastructure.Contracts.Services
{
    public interface ISchedulerProvider
    {
        IScheduler MainThread { get; }
        IScheduler Background { get; }
    }
}
