
using ReactiveUI;

namespace AdionFA.UI.Station.Modules.Trader.ViewModels
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
