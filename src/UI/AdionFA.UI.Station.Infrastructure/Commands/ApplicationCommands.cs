using Prism.Commands;

namespace AdionFA.UI.Infrastructure
{
    public static class ApplicationCommands
    {
        public static CompositeCommand ShowFlyoutCommand { get; } = new();
        public static CompositeCommand LoadProjectsCommand { get; } = new();
        public static CompositeCommand LoadHistoricalDataCommand { get; } = new();

        public static CompositeCommand SaveNodeCommand { get; } = new();
        public static CompositeCommand DeleteNodeCommand { get; } = new();
        public static CompositeCommand AddNodeToMetaTrader { get; } = new();
        public static CompositeCommand RemoveNodeFromMetaTrader { get; } = new();
    }

    public interface IApplicationCommands
    {
        CompositeCommand ShowFlyoutCommand { get; }

        CompositeCommand LoadProjectsCommand { get; }
        CompositeCommand LoadHistoricalDataCommand { get; }

        CompositeCommand SaveNodeCommand { get; }
        CompositeCommand DeleteNodeCommand { get; }
        CompositeCommand AddNodeToMetaTrader { get; }
        CompositeCommand RemoveNodeFromMetaTrader { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand ShowFlyoutCommand => ApplicationCommands.ShowFlyoutCommand;

        public CompositeCommand LoadProjectsCommand => ApplicationCommands.LoadProjectsCommand;
        public CompositeCommand LoadHistoricalDataCommand => ApplicationCommands.LoadHistoricalDataCommand;

        public CompositeCommand SaveNodeCommand => ApplicationCommands.SaveNodeCommand;
        public CompositeCommand DeleteNodeCommand => ApplicationCommands.DeleteNodeCommand;
        public CompositeCommand AddNodeToMetaTrader => ApplicationCommands.AddNodeToMetaTrader;
        public CompositeCommand RemoveNodeFromMetaTrader => ApplicationCommands.RemoveNodeFromMetaTrader;
    }
}