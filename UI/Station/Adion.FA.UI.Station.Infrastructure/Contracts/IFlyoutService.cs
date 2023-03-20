using Adion.FA.UI.Station.Infrastructure.Services;

namespace Adion.FA.UI.Station.Infrastructure.Contracts.Services
{
    public interface IFlyoutService
    {
        void ShowFlyout(FlyoutModel flyoutName);

        bool CanShowFlyout(FlyoutModel flyoutName);
    }
}
