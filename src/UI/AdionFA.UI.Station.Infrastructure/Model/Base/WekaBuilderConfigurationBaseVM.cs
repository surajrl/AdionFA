using AdionFA.UI.Infrastructure.Base;

namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class WekaBuilderConfigurationBaseVM : ViewModelBase
    {
        private int _wekaNTotal;
        public int WekaNTotal
        {
            get => _wekaNTotal;
            set => SetProperty(ref _wekaNTotal, value);
        }

        private int _wekaStartDepth;
        public int WekaStartDepth
        {
            get => _wekaStartDepth;
            set => SetProperty(ref _wekaStartDepth, value);
        }

        private int _wekaEndDepth;
        public int WekaEndDepth
        {
            get => _wekaEndDepth;
            set => SetProperty(ref _wekaEndDepth, value);
        }

        private decimal _wekaMaxRatio;
        public decimal WekaMaxRatio
        {
            get => _wekaMaxRatio;
            set => SetProperty(ref _wekaMaxRatio, value);
        }

    }
}
