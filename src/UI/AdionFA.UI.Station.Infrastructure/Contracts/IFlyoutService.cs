using AdionFA.UI.Infrastructure.Services;

namespace AdionFA.UI.Infrastructure.Contracts.Services
{
    public interface IFlyoutService
    {
        void ShowFlyout(FlyoutModel flyoutName);

        bool CanShowFlyout(FlyoutModel flyoutName);
    }
}
