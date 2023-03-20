﻿using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Project.Model.Configuration;
using FluentValidation;
using System;
using static Adion.FA.Infrastructure.Common.Validators.FluentValidator.FluentValidator;

namespace Adion.FA.UI.Station.Project.Validators.Configuration
{
    public class ProjectConfigurationSettingVMValidator : AbstractValidator<ProjectConfigurationSettingModel>
    {
        public ProjectConfigurationSettingVMValidator()
        {
            #region Market Data

            RuleFor(m => m.MarketDataId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));

            #endregion

            #region Configuration Name

            RuleFor(m => m.Description).NotNull().NotEmpty();

            #endregion

            #region Extractor
            //RuleFor(m => m.Variation).NotNull().NotEmpty();
            #endregion

            #region Period

            RuleFor(m => m.FromDateIS).NotNull().NotEmpty()
                .Must((m, fdis) => fdis < m.ToDateIS && fdis < DateTime.UtcNow).WithMessage(m =>
                        string.Format(ValidationResources.MustBeLessOneAndTwo, ValidationResources.Value, nameof(m.ToDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateIS).NotNull().NotEmpty()
                .Must((m, tdis) => tdis > m.FromDateIS && tdis < DateTime.UtcNow).WithMessage(m =>
                        string.Format(ValidationResources.MustBeGreaterThanOneAndLessThanTow, ValidationResources.Value, nameof(m.FromDateIS), nameof(DateTime.Today)));

            RuleFor(m => m.FromDateOS).NotNull().NotEmpty()
                .Must((m, fdos) => fdos < m.ToDateOS && fdos < DateTime.UtcNow).WithMessage(m =>
                        string.Format(ValidationResources.MustBeLessOneAndTwo, ValidationResources.Value, nameof(m.ToDateOS), nameof(DateTime.Today)));

            RuleFor(m => m.ToDateOS).NotNull().NotEmpty()
                .Must((m, tdos) => tdos > m.FromDateOS && tdos < DateTime.UtcNow).WithMessage(m =>
                    string.Format(ValidationResources.MustBeGreaterThanOneAndLessThanTow, ValidationResources.Value, nameof(m.FromDateOS), nameof(DateTime.Today)));
            
            #endregion

            #region Schedules

            When(m => !m.WithoutSchedule, () => 
            {
                RuleFor(m => m.FromTimeInSecondsAmerica).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAmerica).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsAsia).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsAsia).NotNull().NotEmpty();

                RuleFor(m => m.FromTimeInSecondsEurope).NotNull().NotEmpty();
                RuleFor(m => m.ToTimeInSecondsEurope).NotNull().NotEmpty();
            });

            #endregion

            #region Currency

            RuleFor(m => m.CurrencyPairId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));

            RuleFor(m => m.CurrencyPeriodId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));
            
            RuleFor(m => m.CurrencySpreadId).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));

            #endregion

            #region Weka

            RuleFor(m => m.TotalInstanceWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalDecimalWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.DepthWeka).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustDepthWeka).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinimalSeed).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaximumSeed).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxRatioTree).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMaxRatioTree).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.NTotalTree).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustNTotalTree).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            #endregion

            #region Strategy Builder

            RuleFor(m => m.MinTransactionCountIS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinTransactionCountIS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinPercentSuccessIS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinPercentSuccessIS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));


            RuleFor(m => m.MinTransactionCountOS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinTransactionCountOS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            
            RuleFor(m => m.MinPercentSuccessOS).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustMinPercentSuccessOS).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.VariationTransaction).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustVariationTransaction).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.Progressiveness).GreaterThan(0)
                .When(m => m.IsProgressiveness)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            RuleFor(m => m.MinAdjustProgressiveness).GreaterThan(0)
                .When(m => m.IsProgressiveness && m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MaxPercentCorrelation).GreaterThan(0)
               .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.WinningStrategyTotalUP).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            
            RuleFor(m => m.WinningStrategyTotalDOWN).GreaterThan(0)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));
            
            RuleFor(m => m.MaxAdjustConfig).GreaterThan(0)
                .When(m => m.AutoAdjustConfig)
                .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            #endregion

            #region Assembled Builder

            RuleFor(m => m.TransactionTarget).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.MinAssemblyPercent).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            RuleFor(m => m.TotalAssemblyIterations).GreaterThan(0)
                 .WithMessage(m => string.Format(ValidationResources.NumberGreaterThan, 0));

            #endregion
        }
    }
}
