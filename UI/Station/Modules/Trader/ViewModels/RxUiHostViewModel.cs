
using ReactiveUI;

namespace Adion.FA.UI.Station.Modules.Trader.ViewModels
{
    public class RxUiHostViewModel
    {
        public RxUiHostViewModel(ReactiveObject content)
        {
            Content = content;
        }

        public ReactiveObject Content { get; }
    }
}
