using AdionFA.Application.Contracts;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Common;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Common;
using AdionFA.UI.Infrastructure.Services;
using AutoMapper;
using Ninject;
using Prism.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class GlobalConfigurationViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        private readonly IGlobalConfigurationService _globalConfigurationService;

        public GlobalConfigurationViewModel(IApplicationCommands applicationCommands)
        {
            _globalConfigurationService = IoC.Kernel.Get<IGlobalConfigurationService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutGlobalConfiguration))
            {
                GlobalConfiguration = _mapper.Map<GlobalConfigurationVM>(_globalConfigurationService.GetGlobalConfiguration());
            }
        });

        public ICommand SaveBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                var validationResults = GlobalConfiguration.Validate();
                if (!validationResults.IsValid)
                {
                    MessageHelper.ShowMessagesAsync(this,
                        Resources.GlobalConfiguration,
                        validationResults.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;

                var responseDTO = _globalConfigurationService.UpdateGlobalConfiguration(_mapper.Map<GlobalConfigurationDTO>(GlobalConfiguration));

                IsTransactionActive = false;

                MessageHelper.ShowMessageAsync(this,
                    Resources.GlobalConfiguration,
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

        private GlobalConfigurationVM _globalConfiguration;
        public GlobalConfigurationVM GlobalConfiguration
        {
            get => _globalConfiguration;
            set => SetProperty(ref _globalConfiguration, value);
        }
    }
}