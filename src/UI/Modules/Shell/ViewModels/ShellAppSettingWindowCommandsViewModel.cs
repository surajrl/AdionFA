using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.EventAggregator;
using Prism.Ioc;
using Prism.Events;
using System;

namespace AdionFA.UI.Station.Module.Shell.ViewModels
{
    public class ShellAppSettingWindowCommandsViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        public ShellAppSettingWindowCommandsViewModel()
        {
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<MetaTraderConnectedEvent>().Subscribe(p => IsMetaTraderConnected = p);
        }

        private bool _isMetaTraderConnected;

        public bool IsMetaTraderConnected
        {
            get => _isMetaTraderConnected;
            set => SetProperty(ref _isMetaTraderConnected, value);
        }
    }
}