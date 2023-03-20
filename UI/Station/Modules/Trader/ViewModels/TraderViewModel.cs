using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Commands;
using Adion.FA.UI.Station.Infrastructure.Extensions;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Modules.Trader.Infrastructure;
using Adion.FA.UI.Station.Modules.Trader.Services;
using Dragablz;
using DynamicData;
using DynamicData.Binding;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Modules.Trader.ViewModels
{
    public class TraderViewModel : AbstractNotifyPropertyChanged, IDisposable
    {
        #region Services
        public readonly ITraderService TraderService;
        #endregion

        public event PropertyChangedEventHandler ThemePropertyChanged;
        public IInterTabClient InterTabClient { get; }
        

        #region Ctor
        public TraderViewModel(
            ITraderService traderService,
            IApplicationCommands applicationCommands)
        {
            TraderService = traderService;
            InterTabClient = new InterTabClient();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            //----------------------------------------------
            
            var menuController = Views.ToObservableChangeSet()
                                        .Filter(vc => vc.Content is MainMenuViewModel)
                                        .Transform(vc => (MainMenuViewModel)vc.Content)
                                        .MergeMany(menuItem => menuItem.ItemCreated)
                                        .Subscribe(item =>
                                        {
                                            Views.Add(item);
                                            SelectedContainer = item;
                                        });

            _cleanUp = Disposable.Create(() =>
            {
                menuController.Dispose();
                foreach (var disposable in Views.Select(vc => vc.Content).OfType<IDisposable>())
                    disposable.Dispose();
            });
        }
        #endregion

        #region Dispose
        private readonly IDisposable _cleanUp;
        public void Dispose()
        {
            _cleanUp.Dispose();
        }
        #endregion

        #region Commands
        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutTrader))
            {
                PopulateViewModel();
            }
        }

        public ICommand MemoryCollectCommand { get; } = new Command(() =>
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        });

        public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

        private void ClosingTabItemHandlerImpl(ItemActionCallbackArgs<TabablzControl> args)
        {
            var container = (ViewContainer)args.DragablzItem.DataContext;//.DataContext;
            if (container.Equals(SelectedContainer))
            {
                SelectedContainer = Views.FirstOrDefault(vc => vc != container);
            }
            var disposable = container.Content as IDisposable;
            disposable?.Dispose();
        }

        //public ICommand ShowMenuCommand => new Command(ShowMenu, () => Selected != null && !(Selected.Content is MenuItems));


        public ICommand ApplyMaterialDesignColorCommand { get; } = new AnotherCommandImplementation(o => {
            ApplyPrimary((Swatch)o);
            //ApplyAccent((Swatch)o);
        });
        #endregion

        private void PopulateViewModel()
        {
            #region Material Design Theme
            //----------Set Theme Material Design----------------
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = new Theme();
            try
            {
                theme = paletteHelper.GetTheme();
            }
            catch (Exception ex) 
            {
                theme.SetBaseTheme(Theme.Light);
                paletteHelper.SetTheme(theme);

                Swatch primaryColor = new SwatchesProvider().Swatches.Where(a => a.Name.ToLowerInvariant() == "blue").FirstOrDefault();
                Swatch accentColor = new SwatchesProvider().Swatches.Where(a => a.Name.ToLowerInvariant() == "orange").FirstOrDefault();
                ApplyPrimary(primaryColor);
                ApplyAccent(accentColor);
            }
            //---------------------------------------------
            #endregion

            var existing = Views.FirstOrDefault(vc => vc.Content is MainMenuViewModel);
            if (existing == null)
            {
                var newmenu = new MainMenuViewModel();
                var newItem = new ViewContainer("Menu", newmenu);
                Views.Add(newItem);
                SelectedContainer = newItem;
            }
            else
            {
                SelectedContainer = existing;
            }
        }

        #region Material Design Theme
        private static void ApplyPrimary(Swatch swatch)
        {
            ModifyTheme(theme => {
                theme.SetPrimaryColor(swatch.ExemplarHue.Color);
            });
        }

        private static void ApplyAccent(Swatch swatch)
        {
            if (swatch is { AccentExemplarHue: not null })
            {
                ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
        #endregion

        #region Bindable Model
        private bool istransactionActive;
        public bool IsTransactionActive
        {
            get { return istransactionActive; }
            set { this.SetAndRaise(ref istransactionActive, value); }
        }

        private ViewContainer selectedContainer;
        public ViewContainer SelectedContainer
        {
            get => selectedContainer;
            set => SetAndRaise(ref selectedContainer, value);
        }

        #region Material Design Theme
        private bool isDarkTheme;
        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set
            {
                if (this.MutateVerbose(ref isDarkTheme, value, e => ThemePropertyChanged?.Invoke(this, e)))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }
        public IEnumerable<Swatch> Swatches { get; } = new SwatchesProvider().Swatches;
        #endregion

        public ObservableCollection<ViewContainer> Views { get; } = new ObservableCollection<ViewContainer>();
        #endregion

    }
}
