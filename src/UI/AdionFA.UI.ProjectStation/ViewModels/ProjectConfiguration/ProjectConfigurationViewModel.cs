using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.EventAggregator;
using AdionFA.UI.ProjectStation.Features;
using AutoMapper;
using Ninject;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class ProjectConfigurationViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;

        private readonly IProjectService _projectService;

        private readonly IEventAggregator _eventAggregator;

        public ProjectConfigurationViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectService = IoC.Kernel.Get<IProjectService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.ProjectConfigurationTrim)
            {
                ProjectConfiguration = _mapper.Map<ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));
            }
        });

        public ICommand SaveBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = _projectConfiguration.Validate();
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.StrategyBuilder.GetMetadata().Name,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                await _projectService.UpdateProjectConfigurationAsync(_mapper.Map<ProjectConfigurationDTO>(ProjectConfiguration)).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand RestoreConfigurationBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                var responseDTO = await _projectService.RestoreProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);

                if (responseDTO.IsSuccess)
                {
                    ProjectConfiguration = _mapper.Map<ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));
                }

                IsTransactionActive = false;

                var msg = responseDTO.IsSuccess
                ? Resources.SuccessEntitySave
                : string.IsNullOrEmpty(responseDTO.Message) ? Resources.FailEntitySave : responseDTO.Message;

                MessageHelper.ShowMessage(this,
                    Resources.RestoreProjectConfiguration,
                    msg);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }
    }
}
