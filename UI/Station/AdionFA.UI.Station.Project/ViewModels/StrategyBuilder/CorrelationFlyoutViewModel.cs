using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Project.Model.Weka;
using AdionFA.UI.Station.Project.Services;
using AutoMapper;
using DynamicData;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        // Flyout Command

        private ICommand FlyoutCommand { get; set; }
        private void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelation))
            {
                CorrelationModel = flyoutModel.Model != null ? (CorrelationModel)flyoutModel.Model : new CorrelationModel();
                PopulateViewModel();
            }
        }

        private void PopulateViewModel()
        {
            try
            {
                if (CorrelationModel.Success)
                {
                    NodesUP = new(_mapper.Map<List<REPTreeNodeModel>, List<REPTreeNodeModelVM>>(CorrelationModel.BacktestUP));
                    NodesDOWN = new(_mapper.Map<List<REPTreeNodeModel>, List<REPTreeNodeModelVM>>(CorrelationModel.BacktestDOWN));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<CorrelationFlyoutViewModel>(ex);
                throw;
            }
        }

        // Bindable Model

        private CorrelationModel _correlationModel;
        public CorrelationModel CorrelationModel
        {
            get => _correlationModel;
            set => SetProperty(ref _correlationModel, value);
        }

        private bool _hasTestInMetatraderUP;
        public bool HasTestInMetatraderUP
        {
            get => _hasTestInMetatraderUP;
            set => SetProperty(ref _hasTestInMetatraderUP, value);
        }

        private ObservableCollection<REPTreeNodeModelVM> _nodesUP;
        public ObservableCollection<REPTreeNodeModelVM> NodesUP
        {
            get => _nodesUP;
            set => SetProperty(ref _nodesUP, value);
        }

        private bool _hasTestInMetatraderDOWN;
        public bool HasTestInMetatraderDOWN
        {
            get => _hasTestInMetatraderDOWN;
            set => SetProperty(ref _hasTestInMetatraderDOWN, value);
        }

        private ObservableCollection<REPTreeNodeModelVM> _nodesDown;
        public ObservableCollection<REPTreeNodeModelVM> NodesDOWN
        {
            get => _nodesDown;
            set => SetProperty(ref _nodesDown, value);
        }
    }
}
