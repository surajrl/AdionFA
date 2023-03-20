using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Helpers;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Helpers;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using Adion.FA.UI.Station.Module.Dashboard.Services;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Module.Dashboard.ViewModels
{
    public class UploadMarketDataViewModel : ViewModelBase
    {
        #region Services

        public readonly ISettingService SettingService;

        #endregion

        #region Ctor

        public UploadMarketDataViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            SettingService = settingService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        #endregion

        #region Commands

        ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutUploadMarketData))
            {
                PopulateViewModel();
            }
        }

        public DelegateCommand UploadBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;

                var validator = marketData.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.MarketData.GetMetadata().Description,
                            validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                if (CreateHistory())
                {
                    var result = await SettingService.CreateMarketData(marketData);
                    
                    IsTransactionActive = false;

                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.MarketData.GetMetadata().Description,
                        result ? MessageResources.EntitySaveSuccess : MessageResources.EntityErrorTransaction);

                    PopulateViewModel();

                    if (result)
                    {
                        ContainerLocator.Current.Resolve<IApplicationCommands>().LoadMarketData.Execute(null);
                    }
                }
                else
                {
                    IsTransactionActive = false;
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        
        bool CreateHistory()
        {
            try
            {
                List<Candle> result = CandleHelper.GetHistoryCandles(marketData.PathFileMarketData).ToList();

                if (result.Count > 0)
                {

                    result.ForEach(item =>
                    {
                        marketData.MarketDataDetails.Add(new MarketDataDetailVM
                        {
                            StartDate = item.date,
                            StartTime = item.time,
                            OpenPrice = item.open,
                            MaxPrice = item.max,
                            MinPrice = item.min,
                            ClosePrice = item.close,
                            Volumen = item.volumen
                        });
                    });

                    string marketName = ((MarketEnum)marketData.MarketId).GetMetadata().Name;
                    string pairName = ((CurrencyPairEnum)marketData.CurrencyPairId).GetMetadata().Name;
                    string periodName = ((CurrencyPeriodEnum)marketData.CurrencyPeriodId).GetMetadata().Code;
                    marketData.Description = $"{marketName}.{pairName}.{periodName}";

                    return true;
                }

                MessageHelper.ShowMessage(this, 
                    EntityTypeEnum.MarketData.GetMetadata().Description, 
                    MessageResources.MarketDataFileEmpty);

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion

        void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                MarketData = new UploadMarketDataModel 
                {
                    MarketDataDetails = Array.Empty<MarketDataDetailVM>().ToList() 
                };
            }

            if (!Markets.Any())
                Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());

            if (!CurrencyPairs.Any())
                CurrencyPairs.AddRange(EnumUtil.ToEnumerable<CurrencyPairEnum>());

            if (!CurrencyPeriods.Any())
                CurrencyPeriods.AddRange(EnumUtil.ToEnumerable<CurrencyPeriodEnum>());
        }

        #region Bindable Model

        bool istransactionActive;
        public bool IsTransactionActive
        {
            get { return istransactionActive; }
            set { SetProperty(ref istransactionActive, value); }
        }

        UploadMarketDataModel marketData;
        public UploadMarketDataModel MarketData 
        { 
            get => marketData;
            set => SetProperty(ref marketData, value); 
        }

        public ObservableCollection<Metadata> Markets { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencyPairs { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencyPeriods { get; } = new ObservableCollection<Metadata>();

        #endregion
    }
}
