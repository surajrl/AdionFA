using Prism.Commands;

namespace Adion.FA.UI.Station.Infrastructure
{
    public static class ApplicationCommands
    {
        public static CompositeCommand LoadedProjectCommand = new CompositeCommand();
        public static CompositeCommand StartProcessProjectCommand = new CompositeCommand();
        public static CompositeCommand EndAllProcessProjectCommand = new CompositeCommand();
        public static CompositeCommand ShowFlyoutCommand = new CompositeCommand();
        public static CompositeCommand LoadProjectHierarchyCommand = new CompositeCommand();
        public static CompositeCommand NodeTestInMetatraderCommand = new CompositeCommand();
        public static CompositeCommand LoadMarketData = new CompositeCommand();
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
        CompositeCommand LoadMarketData { get; }
        CompositeCommand PinnedProjectCommand { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand LoadedProjectCommand
        {
            get { return ApplicationCommands.LoadedProjectCommand; }
        }

        public CompositeCommand StartProcessProjectCommand
        {
            get { return ApplicationCommands.StartProcessProjectCommand; }
        }

        public CompositeCommand EndAllProcessProjectCommand
        {
            get { return ApplicationCommands.EndAllProcessProjectCommand; }
        }

        public CompositeCommand ShowFlyoutCommand
        {
            get { return ApplicationCommands.ShowFlyoutCommand; }
        }

        public CompositeCommand LoadProjectHierarchyCommand
        {
            get { return ApplicationCommands.LoadProjectHierarchyCommand; }
        }

        public CompositeCommand NodeTestInMetatraderCommand
        {
            get { return ApplicationCommands.NodeTestInMetatraderCommand; }
        }

        public CompositeCommand LoadMarketData
        {
            get => ApplicationCommands.LoadMarketData;
        }

        public CompositeCommand PinnedProjectCommand
        { 
            get => ApplicationCommands.PinnedProjectCommand;
        }
    }
}
