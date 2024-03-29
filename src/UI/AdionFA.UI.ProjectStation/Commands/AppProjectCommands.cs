﻿using Prism.Commands;

namespace AdionFA.UI.ProjectStation.Commands
{
    public static class AppProjectCommands
    {
        public readonly static CompositeCommand SelectItemHamburgerMenuCommand = new();
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
