using AdionFA.UI.Station.Infrastructure.Services;

namespace AdionFA.UI.Station.Infrastructure.Contracts.Services
{
    public interface IFlyoutService
    {
        void ShowFlyout(FlyoutModel flyoutName);

        bool CanShowFlyout(FlyoutModel flyoutName);
    }
}
