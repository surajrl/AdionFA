using Prism.Commands;

namespace AdionFA.UI.Station.Infrastructure
{
    public static class ApplicationCommands
    {
        public static CompositeCommand LoadedProjectCommand = new CompositeCommand();
        public static CompositeCommand StartProcessProjectCommand = new CompositeCommand();
        public static CompositeCommand EndAllProcessProjectCommand = new CompositeCommand();
        public static CompositeCommand ShowFlyoutCommand = new CompositeCommand();
        public static CompositeCommand LoadProjectHierarchyCommand = new CompositeCommand();
        public static CompositeCommand NodeTestInMetatraderCommand = new CompositeCommand();
        public static CompositeCommand LoadHistoricalData = new CompositeCommand();
        public static CompositeCommand PinnedProjectCommand = new CompositeCommand();
    }

    public interface IApplicationCommands
    {
        CompositeCommand LoadedProjectCommand { get; }
        CompositeCommand StartProcessProjectCommand { get; }
        CompositeCommand EndAllProcessProjectCommand { get; }
        CompositeCommand ShowFlyoutCommand { get; }
        CompositeCommand LoadProjectHierarchyCommand { get; }
        CompositeCommand NodeTestInMetatraderCommand { get;  }
        CompositeCommand LoadHistoricalData { get; }
        CompositeCommand PinnedProjectCommand { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand LoadedProjectCommand
        {
           get => ApplicationCommands.LoadedProjectCommand;
        }

        public CompositeCommand StartProcessProjectCommand
        {
            get => ApplicationCommands.StartProcessProjectCommand;
        }

        public CompositeCommand EndAllProcessProjectCommand
        {
            get => ApplicationCommands.EndAllProcessProjectCommand;
        }

        public CompositeCommand ShowFlyoutCommand
        {
            get => ApplicationCommands.ShowFlyoutCommand;
        }

        public CompositeCommand LoadProjectHierarchyCommand
        {
            get => ApplicationCommands.LoadProjectHierarchyCommand;
        }

        public CompositeCommand NodeTestInMetatraderCommand
        {
            get => ApplicationCommands.NodeTestInMetatraderCommand;
        }

        public CompositeCommand LoadHistoricalData
        {
            get => ApplicationCommands.LoadHistoricalData;
        }

        public CompositeCommand PinnedProjectCommand
        { 
            get => ApplicationCommands.PinnedProjectCommand;
        }
    }
}
