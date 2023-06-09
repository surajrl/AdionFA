﻿using Prism.Commands;

namespace AdionFA.UI.Station.Project.Commands
{
    public static class AppProjectCommands
    {
        public static CompositeCommand SelectItemHamburgerMenuCommand = new CompositeCommand();
    }

    public interface IAppProjectCommands
    {
        CompositeCommand SelectItemHamburgerMenuCommand { get; }
    }

    public class AppProjectCommandsProxy : IAppProjectCommands
    {
        public CompositeCommand SelectItemHamburgerMenuCommand
        {
            get => AppProjectCommands.SelectItemHamburgerMenuCommand;
        }
    }
}
