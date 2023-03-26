using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Services;
using AutoMapper;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        public readonly IMapper Mapper;

        private readonly IProjectServiceAgent _projectService;

        private ProjectVM Project;

        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

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

        #endregion Commands

        public async void PopulateViewModel()
        {
            try
            {
                Project = await _projectService.GetProject(ProcessArgs.ProjectId, true);

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

        #endregion Bindable Model
    }
}