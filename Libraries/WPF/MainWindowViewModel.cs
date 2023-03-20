using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF
{
    internal class MainWindowViewModel
    {
        #region Bindable Properties

        public PersonModel Person { get; set; }

        #endregion

        #region Ctor

        public MainWindowViewModel()
        {
        }

        #endregion

        #region Commands

        public ICommand Validate => new ValidateCommand();

        public CommandBinding ValidateCommandBinding => new CommandBinding(AppRouteCommands.ValidateCommand, (target, arg) => 
        {
            var element = ((FrameworkElement)target);
            MessageBox.Show(element.Name);

        }, (send, arg) => 
        {
            var controlTarget = arg.Source as Control;
            if (controlTarget != null)
                arg.CanExecute = true;
            else
                arg.CanExecute = false;
        });

        #endregion
    }


    #region Commands

    public static class AppRouteCommands
    {
        public static RoutedCommand ValidateCommand = new RoutedCommand();
    }
    
    public static class AppRouteUICommands
    {
        public static RoutedUICommand ValidateCommand = new RoutedUICommand();
    }

    internal class ValidateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("OK");
        }
    }

    #endregion

    #region Model

    public class PersonModel : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        private int age;
        public int Age
        {
            get => age;
            set { age = value; OnPropertyChanged(); }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    #endregion
}
