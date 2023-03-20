using Dragablz;
using Prism.Ioc;
using System.Windows;

namespace Adion.FA.UI.Station.Modules.Trader.Infrastructure
{
    public class InterTabClient : IInterTabClient
    {
        public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
        {
            var window = ContainerLocator.Current.Resolve<IWindowFactory>().Create();

            return new NewTabHost<Window>(window, window.InitialTabablzControl);
        }

        public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        }
    }
}
