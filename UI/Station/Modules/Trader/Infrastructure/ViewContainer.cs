using DynamicData.Binding;
using System;

namespace AdionFA.UI.Station.Modules.Trader.Infrastructure
{
    public class ViewContainer : AbstractNotifyPropertyChanged
    {
        public ViewContainer(string title, object content)
        {
            Title = title;
            Content = content;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; }
        public object Content { get; }
    }
}
