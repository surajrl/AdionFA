using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Regions;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Infrastructure.Services
{
    public class FlyoutModel
    {
        public string Name { get; set; }
        public object ModelOne { get; set; }
        public object ModelTwo { get; set; }
    }

    public class FlyoutService : IFlyoutService
    {
        private readonly IRegionManager _regionManager;

        public ICommand ShowFlyoutCommand { get; set; }

        public FlyoutService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            ShowFlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout, CanShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(ShowFlyoutCommand);
        }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            var region = _regionManager.Regions[FlyoutRegions.FlyoutRegion];

            if (region != null)
            {
                if (region.Views.FirstOrDefault(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(flyoutModel.Name)) is Flyout flyout)
                {
                    flyout.IsOpen = !flyout.IsOpen;
                }
            }
        }

        public bool CanShowFlyout(FlyoutModel flyoutName)
        {
            return true;
        }
    }
}