using Adion.FA.Infrastructure.Common.Helpers;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using Adion.FA.UI.Station.Module.Dashboard.Services;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Module.Dashboard.ViewModels
{
    public class MarketDataViewModel : ViewModelBase
    {
        #region Services

        public readonly ISettingService SettingService;

        #endregion

        #region Ctor

        public MarketDataViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            SettingService = settingService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            applicationCommands.LoadMarketData.RegisterCommand(MarketDataFilterCommand);
        }

        #endregion

        #region Commands

        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutMarketData))
            {
                PopulateViewModel();
            }
        }

        public ICommand MarketDataFilterCommand => new DelegateCommand(LoadMarketData);

        #endregion

        private void PopulateViewModel()
        {
            try
            {
                MarketId = (int)MarketEnum.Forex;
                CurrencyPairId = (int)CurrencyPairEnum.EURUSD;
                CurrencyPeriodId = (int)CurrencyPeriodEnum.H1;

                LoadMarketData();

                if (!Markets.Any())
                    Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());

                if (!CurrencyPairs.Any())
                    CurrencyPairs.AddRange(EnumUtil.ToEnumerable<CurrencyPairEnum>());

                if (!CurrencyPeriods.Any())
                    CurrencyPeriods.AddRange(EnumUtil.ToEnumerable<CurrencyPeriodEnum>());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        async void LoadMarketData()
        {
            try
            {
                IsTransactionActive = true;
                MarketData?.Clear();
                var vm = await SettingService.GetMarketData(
                                    marketId ?? (int)MarketEnum.Forex,
                                    currencyPairId ?? (int)CurrencyPairEnum.EURUSD,
                                    currencyPeriodId ?? (int)CurrencyPeriodEnum.H1);

                MarketData = new ObservableCollection<MarketDataDetailSettingVM>(vm.MarketDataDetailSettings);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
            }
        }

        #region Bindable Model

        private bool istransactionActive;
        public bool IsTransactionActive
        {
            get { return istransactionActive; }
            set { this.SetProperty<bool>(ref this.istransactionActive, value); }
        }

        private int? marketId;
        public int? MarketId
        {
            get { return marketId; }
            set { this.SetProperty<int?>(ref this.marketId, value); }
        }

        private int? currencyPairId;
        public int? CurrencyPairId
        {
            get { return currencyPairId; }
            set { this.SetProperty<int?>(ref this.currencyPairId, value); }
        }

        private int? currencyPeriodId;
        public int? CurrencyPeriodId
        {
            get { return currencyPeriodId; }
            set { this.SetProperty<int?>(ref this.currencyPeriodId, value); }
        }

        private ObservableCollection<MarketDataDetailSettingVM> marketData;
        public ObservableCollection<MarketDataDetailSettingVM> MarketData 
        {
            get { return marketData; }
            set { SetProperty<ObservableCollection<MarketDataDetailSettingVM>>(ref marketData, value); }
        } 

        public ObservableCollection<Metadata> Markets { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencyPairs { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencyPeriods { get; } = new ObservableCollection<Metadata>();

        #endregion
    }
}
