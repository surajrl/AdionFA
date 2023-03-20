using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using Adion.FA.Infrastructure.Common.Logger.Helpers;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Project.AutoMapper;
using Adion.FA.UI.Station.Project.Services;
using AutoMapper;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        #region AutoMapper
        public readonly IMapper Mapper;
        #endregion

        #region Services
        private readonly IAppProjectService AppProjectService;
        #endregion

        private ProjectVM Project;

        #region Ctor
        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }
        #endregion

        #region Commands
        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelation))
            {
                CorrelationModel = flyoutModel.Model != null ? (CorrelationModel)flyoutModel.Model : new CorrelationModel();
                PopulateViewModel();
            }
        }
        #endregion

        public async void PopulateViewModel()
        {
            try
            {
                Project = await AppProjectService.GetProject(ProcessArgs.ProjectId, true);

                if (CorrelationModel.Success)
                {
                    UPNodes = new ObservableCollection<BacktestModel>(CorrelationModel.BacktestUP);
                    DOWNNodes = new ObservableCollection<BacktestModel>(CorrelationModel.BacktestDOWN);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<CorrelationFlyoutViewModel>(ex);
                throw;
            }
        }

        #region Bindable Model
        private CorrelationModel correlationModel;
        public CorrelationModel CorrelationModel
        {
            get => correlationModel;
            set => SetProperty(ref correlationModel, value);
        }


        private bool hasTestInMetatraderUP;
        public bool HasTestInMetatraderUP 
        {
            get => hasTestInMetatraderUP; 
            set => SetProperty(ref hasTestInMetatraderUP, value); 
        }


        private ObservableCollection<BacktestModel> uPNodes;
        public ObservableCollection<BacktestModel> UPNodes
        {
            get => uPNodes;
            set => SetProperty(ref uPNodes, value);
        }


        private bool hasTestInMetatraderDOWN;
        public bool HasTestInMetatraderDOWN
        {
            get => hasTestInMetatraderDOWN;
            set => SetProperty(ref hasTestInMetatraderDOWN, value);
        }


        private ObservableCollection<BacktestModel> dOWNNodes;
        public ObservableCollection<BacktestModel> DOWNNodes
        {
            get => dOWNNodes;
            set => SetProperty(ref dOWNNodes, value);
        }
        #endregion
    }
}
