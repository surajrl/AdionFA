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
        public string FlyoutName { get; set; }
        public object Model { get; set; }
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
                var flyout = region.Views.Where(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(flyoutModel.FlyoutName)).FirstOrDefault() as Flyout;

                if (flyout != null)
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