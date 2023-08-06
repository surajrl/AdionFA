using AdionFA.Infrastructure.AssemblyBuilder.Model;
using Prism.Events;

namespace AdionFA.UI.ProjectStation.EventAggregator
{
    public class AssemblyBuilderCompletedEvent : PubSubEvent<AssemblyBuilderModel>
    {
    }
}
