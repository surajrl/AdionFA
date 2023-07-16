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
            var vr = v.Validate((T)this);
            vr.Errors.ForEach(error =>
            {
                SetError(error.PropertyName, error.ErrorMessage);
            });
            return vr;
        }

        public ValidationResult Validate()
        {
            ClearAllErrors();
            var vr = (this as IModelValidator).GetValidationResult();
            vr.Errors.ForEach(error =>
            {
                SetError(error.PropertyName, error.ErrorMessage);
            });

            return vr;
        }
    }
}