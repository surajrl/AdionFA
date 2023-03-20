﻿using FluentValidation;
using FluentValidation.Results;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace Adion.FA.UI.Station.Infrastructure.Base
{
    public abstract class ViewModelBase : ValidatableBindableBase
    {
        public ViewModelBase()
        {
            RegionManager = ContainerLocator.Current.Resolve<IRegionManager>();
            EventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
        }
        
        #region RegionManager
        private IRegionManager regionManager;
        public IRegionManager RegionManager 
        {
            get { return regionManager; }
            private set { SetProperty(ref regionManager, value); } 
        }
        #endregion

        #region EventAggregator
        private IEventAggregator eventAggregator;
        public IEventAggregator EventAggregator
        {
            get { return eventAggregator; }
            private set { SetProperty(ref eventAggregator, value); }
        }
        #endregion

        #region Validation

        public ValidationResult Validate<T>(AbstractValidator<T> v) where T : ViewModelBase
        {
            ClearAllErrors();
            ValidationResult vr = v.Validate((T)this);
            vr.Errors.ForEach(error =>
            {
                SetError(error.PropertyName, error.ErrorMessage);
            });
            return vr;
        }

        public ValidationResult Validate()
        {
            ClearAllErrors();
            ValidationResult vr = (this as IModelValidator).GetValidationResult();
            vr.Errors.ForEach(error =>
            {
                SetError(error.PropertyName, error.ErrorMessage);
            });

            return vr;
        }

        #endregion
    }
}
