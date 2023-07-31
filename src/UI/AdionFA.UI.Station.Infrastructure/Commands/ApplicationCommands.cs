using Prism.Commands;

namespace AdionFA.UI.Infrastructure
{
    public static class ApplicationCommands
    {
        public static CompositeCommand LoadedProjectCommand { get; } = new();
        public static CompositeCommand PinnedProjectCommand { get; } = new();

        public static CompositeCommand StartProcessProjectCommand { get; } = new();
        public static CompositeCommand EndAllProcessProjectCommand { get; } = new();

        public static CompositeCommand ShowFlyoutCommand { get; } = new();
        public static CompositeCommand LoadProjectHierarchyCommand { get; } = new();
        public static CompositeCommand LoadHistoricalDataCommand { get; } = new();

        public static CompositeCommand SaveNodeCommand { get; } = new();
        public static CompositeCommand DeleteNodeCommand { get; } = new();
        public static CompositeCommand AddNodeToMetaTrader { get; } = new();
        public static CompositeCommand RemoveNodeFromMetaTrader { get; } = new();
    }

    public interface IApplicationCommands
    {
        CompositeCommand LoadedProjectCommand { get; }
        CompositeCommand PinnedProjectCommand { get; }

        CompositeCommand StartProcessProjectCommand { get; }
        CompositeCommand EndAllProcessProjectCommand { get; }

        CompositeCommand ShowFlyoutCommand { get; }

        CompositeCommand LoadProjectHierarchyCommand { get; }
        CompositeCommand LoadHistoricalDataCommand { get; }

        CompositeCommand SaveNodeCommand { get; }
        CompositeCommand DeleteNodeCommand { get; }
        CompositeCommand AddNodeToMetaTrader { get; }
        CompositeCommand RemoveNodeFromMetaTrader { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand LoadedProjectCommand => ApplicationCommands.LoadedProjectCommand;
        public CompositeCommand PinnedProjectCommand => ApplicationCommands.PinnedProjectCommand;

        public CompositeCommand StartProcessProjectCommand => ApplicationCommands.StartProcessProjectCommand;
        public CompositeCommand EndAllProcessProjectCommand => ApplicationCommands.EndAllProcessProjectCommand;

        public CompositeCommand ShowFlyoutCommand => ApplicationCommands.ShowFlyoutCommand;

        public CompositeCommand LoadProjectHierarchyCommand => ApplicationCommands.LoadProjectHierarchyCommand;
        public CompositeCommand LoadHistoricalDataCommand => ApplicationCommands.LoadHistoricalDataCommand;

        public CompositeCommand SaveNodeCommand => ApplicationCommands.SaveNodeCommand;
        public CompositeCommand DeleteNodeCommand => ApplicationCommands.DeleteNodeCommand;
        public CompositeCommand AddNodeToMetaTrader => ApplicationCommands.AddNodeToMetaTrader;
        public CompositeCommand RemoveNodeFromMetaTrader => ApplicationCommands.RemoveNodeFromMetaTrader;
    }
}