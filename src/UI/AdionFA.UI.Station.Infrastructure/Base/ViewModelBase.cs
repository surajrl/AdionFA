using FluentValidation;
using FluentValidation.Results;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace AdionFA.UI.Infrastructure.Base
{
    public abstract class ViewModelBase : ValidatableBindableBase
    {
        public ViewModelBase()
        {
            RegionManager = ContainerLocator.Current.Resolve<IRegionManager>();
            EventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
        }

        // Region Manager

        private IRegionManager _regionManager;
        public IRegionManager RegionManager
        {
            get => _regionManager;
            private set => SetProperty(ref _regionManager, value);
        }

        // Event Aggregator

        private IEventAggregator _eventAggregator;
        public IEventAggregator EventAggregator
        {
            get => _eventAggregator;
            private set => SetProperty(ref _eventAggregator, value);
        }

        // Validation

        public ValidationResult Validate<T>(AbstractValidator<T> v) where T : ViewModelBase
        {
            ClearAllErrors();
            var validationResult = v.Validate((T)this);
            validationResult.Errors.ForEach(error =>
            {
                SetError(error.PropertyName, error.ErrorMessage);
            });
            return validationResult;
        }

        public ValidationResult Validate()
        {
            ClearAllErrors();
            var validationResult = (this as IModelValidator).GetValidationResult();
            validationResult.Errors.ForEach(error =>
            {
                SetError(error.PropertyName, error.ErrorMessage);
            });

            return validationResult;
        }
    }
}