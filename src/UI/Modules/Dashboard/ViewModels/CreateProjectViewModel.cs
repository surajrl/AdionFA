using AdionFA.Application.Contracts;
using AdionFA.Domain.Model;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.Infrastructure.Services;
using AutoMapper;
using Ninject;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class CreateProjectViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        private readonly IMarketDataService _marketDataService;
        private readonly IProjectService _projectService;

        public CreateProjectViewModel(IApplicationCommands applicationCommands)
        {
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();
            _projectService = IoC.Kernel.Get<IProjectService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            HistoricalData = new ObservableCollection<Metadata>();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
            {
                if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutCreateProject))
                {
                    if (!IsTransactionActive)
                    {
                        Project = new();

                        var allHistoricalDataDTO = _marketDataService.GetAllHistoricalData(false);

                        HistoricalData.Clear();
                        HistoricalData.AddRange(allHistoricalDataDTO
                            .Select(historicalDataDTO => new Metadata
                            {
                                Id = historicalDataDTO.HistoricalDataId,
                                Name = historicalDataDTO.Description
                            }).ToList());
                    }
                }
            });

        public ICommand CreateProjectBtnCommand => new DelegateCommand(() =>
        {
            var validationResult = Project.Validate();
            if (!validationResult.IsValid)
            {
                MessageHelper.ShowMessages(this,
                    Resources.CreateProject,
                    validationResult.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            try
            {
                IsTransactionActive = true;

                var responseDTO = _projectService.CreateProject(_mapper.Map<ProjectDTO>(Project));

                if (responseDTO.IsSuccess)
                {
                    ContainerLocator.Current.Resolve<IApplicationCommands>().LoadProjectsCommand.Execute(null);
                }

                IsTransactionActive = false;

                MessageHelper.ShowMessage(this,
                    Resources.CreateProject,
                    responseDTO.IsSuccess
                    ? Resources.SuccessEntitySave
                    : Resources.FailEntitySave);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private ProjectVM _project;
        public ProjectVM Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        public ObservableCollection<Metadata> HistoricalData { get; }
    }
}