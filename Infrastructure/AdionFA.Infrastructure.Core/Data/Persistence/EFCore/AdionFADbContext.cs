using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Aggregates.MetaTrader;
using AdionFA.Core.Domain.Aggregates.Organization;
using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Core.Domain.Aggregates.ReferenceData;

using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TALib;

namespace AdionFA.Infrastructure.Core.Data.Persistence
{
    public class AdionFADbContext : DbContext, IAdionFADbContext
    {
        private readonly string _connectionString;

        public AdionFADbContext()
        {
            _connectionString = AppSettingsManager.Instance.Get<AppSettings>().DefaultConnection;
        }

        public AdionFADbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);

#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging().UseLoggerFactory(LoggerFactory.Create(b => b.AddDebug()));
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity Configuration

            Assembly assembly = Assembly.GetExecutingAssembly();

            List<Type> types = assembly.GetTypes().Where(
                t => !t.IsInterface &&
                     typeof(IAdionFAETC).IsAssignableFrom(t)).ToList();

            types.ForEach(t =>
            {
                var genericType = t.GetInterface(typeof(IEntityTypeConfiguration<>).FullName).GetGenericArguments().Single();
                var entityConfiguration = assembly.CreateInstance(t.FullName);
                var m = typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.ApplyConfiguration));
                m.MakeGenericMethod(genericType).Invoke(modelBuilder, new object[] { entityConfiguration });
            });

            MethodInfo m = typeof(EnumExtension).GetMethod("GetMetadata");

            var userId = "11111111-1111-1111-11111111111111111";
            var username = "sysadmin";
            var tenantId = "22222222-2222-2222-2222-222222222222";

            // Entity Type

            foreach (var et in Enum.GetValues(typeof(EntityTypeEnum)))
            {
                var meta = (Metadata)m.Invoke(et, new object[] { et });
                modelBuilder.Entity<EntityType>().HasData(
                    new EntityType
                    {
                        EntityTypeId = (int)et,
                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Setting

            foreach (var setting in Enum.GetValues(typeof(SettingEnum)))
            {
                var meta = (Metadata)m.Invoke(setting, new object[] { setting });
                modelBuilder.Entity<Setting>().HasData(
                    new Setting
                    {
                        SettingId = (int)setting,

                        Code = meta.Code,
                        Value = meta.Value,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Symbol

            modelBuilder.Entity<Symbol>().HasData(
                new Symbol
                {
                    SymbolId = 1,

                    Code = "EURUSD",
                    Name = "EURUSD",
                    Description = "Euro vs US Dollar",

                    IsDeleted = false,
                    Inaccesible = false,
                    TenantId = tenantId,
                    CreatedById = userId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByUserName = username
                });

            // Currency Spread

            foreach (var csp in Enum.GetValues(typeof(CurrencySpreadEnum)))
            {
                var meta = (Metadata)m.Invoke(csp, new object[] { csp });
                modelBuilder.Entity<CurrencySpread>().HasData(
                    new CurrencySpread
                    {
                        CurrencySpreadId = (int)csp,

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Timeframe

            foreach (var timeframe in Enum.GetValues(typeof(TimeframeEnum)))
            {
                var meta = (Metadata)m.Invoke(timeframe, new object[] { timeframe });
                modelBuilder.Entity<Timeframe>().HasData(
                    new Timeframe
                    {
                        TimeframeId = (int)timeframe,

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Market

            foreach (var market in Enum.GetValues(typeof(MarketEnum)))
            {
                var meta = (Metadata)m.Invoke(market, new object[] { market });
                modelBuilder.Entity<Market>().HasData(
                    new Market
                    {
                        MarketId = (int)market,

                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Market Region

            foreach (var mr in Enum.GetValues(typeof(MarketRegionEnum)))
            {
                var meta = (Metadata)m.Invoke(mr, new object[] { mr });
                modelBuilder.Entity<MarketRegion>().HasData(
                    new MarketRegion
                    {
                        MarketRegionId = (int)mr,

                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Organization

            modelBuilder.Entity<Organization>().HasData(new Organization
            {
                OrganizationId = tenantId,

                Name = "AdionFA",
                LegalName = "AdionFA",

                IsDeleted = false,
                Inaccesible = false,
                TenantId = tenantId,
                CreatedById = userId,
                CreatedOn = DateTime.UtcNow,
                CreatedByUserName = username
            });

            // Project Global Configuration

            modelBuilder.Entity<ProjectGlobalConfiguration>().HasData(new ProjectGlobalConfiguration
            {
                ProjectGlobalConfigurationId = 1,

                // Extractor

                Variation = 50,

                // Period

                FromDateIS = null,
                ToDateIS = null,

                FromDateOS = null,
                ToDateOS = null,

                // Schedule

                WithoutSchedule = true,

                // Historical Data Information

                SymbolId = 1,
                TimeframeId = (int)TimeframeEnum.H1,
                CurrencySpreadId = (int)CurrencySpreadEnum.Five,

                // Weka

                TotalInstanceWeka = 1,

                DepthWeka = 6,
                MinAdjustDepthWeka = 5,

                TotalDecimalWeka = 5,
                MinimalSeed = 100,
                MaximumSeed = 1000000,

                MaxRatioTree = (decimal)1.5,
                MinAdjustMaxRatioTree = 1,
                NTotalTree = 300,
                MinAdjustNTotalTree = 150,

                // Strategy Builder

                MinTransactionCountIS = 300,
                MinAdjustMinTransactionCountIS = 360,
                MinPercentSuccessIS = 55,
                MinAdjustMinPercentSuccessIS = 50,

                MinTransactionCountOS = 100,
                MinAdjustMinTransactionCountOS = 180,
                MinPercentSuccessOS = 55,
                MinAdjustMinPercentSuccessOS = 50,

                VariationTransaction = 4,
                MinAdjustVariationTransaction = 4,

                Progressiveness = 2,
                MinAdjustProgressiveness = 2,
                IsProgressiveness = false,

                MaxPercentCorrelation = 2,

                WinningStrategyTotalUP = 6,
                WinningStrategyTotalDOWN = 6,

                AutoAdjustConfig = false,
                MaxAdjustConfig = 5,

                // Assembled Builder

                TransactionTarget = 600,
                MinAssemblyPercent = 5,
                TotalAssemblyIterations = 1,

                // Time Sensitive Base

                StartDate = DateTime.UtcNow,
                EndDate = null,
                Description = "Default Configuration",

                // Entity Base

                IsDeleted = false,
                Inaccesible = false,
                TenantId = tenantId,
                CreatedById = userId,
                CreatedOn = DateTime.UtcNow,
                CreatedByUserName = username
            });

            // Project Global Schedule Configuration

            modelBuilder.Entity<ProjectGlobalScheduleConfiguration>().HasData(
                new ProjectGlobalScheduleConfiguration
                {
                    Description = "Default Schedule America",
                    ProjectGlobalScheduleConfigurationId = 1,
                    ProjectGlobalConfigurationId = 1,

                    MarketRegionId = (int)MarketRegionEnum.America,
                    FromTimeInSeconds = 54000,
                    ToTimeInSeconds = 82800,

                    StartDate = DateTime.UtcNow,
                    EndDate = null,

                    IsDeleted = false,
                    Inaccesible = false,
                    TenantId = tenantId,
                    CreatedById = userId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByUserName = username
                },
                new ProjectGlobalScheduleConfiguration
                {
                    Description = "Default Schedule Europe",
                    ProjectGlobalScheduleConfigurationId = 2,
                    ProjectGlobalConfigurationId = 1,

                    MarketRegionId = (int)MarketRegionEnum.Europe,
                    FromTimeInSeconds = 32400,
                    ToTimeInSeconds = 64800,

                    StartDate = DateTime.UtcNow,
                    EndDate = null,

                    IsDeleted = false,
                    Inaccesible = false,
                    TenantId = tenantId,
                    CreatedById = userId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByUserName = username
                },
                new ProjectGlobalScheduleConfiguration
                {
                    Description = "Default Schedule Asia",
                    ProjectGlobalScheduleConfigurationId = 3,
                    ProjectGlobalConfigurationId = 1,

                    MarketRegionId = (int)MarketRegionEnum.Asia,
                    FromTimeInSeconds = 3600,
                    ToTimeInSeconds = 32400,

                    StartDate = DateTime.UtcNow,
                    EndDate = null,

                    IsDeleted = false,
                    Inaccesible = false,
                    TenantId = tenantId,
                    CreatedById = userId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByUserName = username
                }
            );

            // Project Step

            foreach (var ps in Enum.GetValues(typeof(ProjectStepEnum)))
            {
                var meta = (Metadata)m.Invoke(ps, new object[] { ps });
                modelBuilder.Entity<ProjectStep>().HasData(
                    new ProjectStep
                    {
                        ProjectStepId = (int)ps,
                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            // Currencies

            foreach (var c in Enum.GetValues(typeof(CurrencyEnum)))
            {
                var meta = (Metadata)m.Invoke(c, new object[] { c });
                modelBuilder.Entity<Currency>().HasData(
                    new Currency
                    {
                        CurrencyId = (int)c,

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,
                        Description = meta.Description,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }
        }

        // Common

        public DbSet<EntityServiceHost> EntityServiceHosts { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Setting> Settings { get; set; }

        // Market Data

        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
        public DbSet<CurrencySpread> CurrencySpreads { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<HistoricalData> HistoricalDatas { get; set; }
        public DbSet<HistoricalDataCandle> HistoricalDataCandles { get; set; }
        public DbSet<MarketRegion> MarketRegions { get; set; }

        // MetaTrader

        public DbSet<ExpertAdvisor> ExpertAdvisors { get; set; }

        // Project

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }
        public DbSet<ProjectGlobalConfiguration> ProjectGlobalConfigurations { get; set; }
        public DbSet<ProjectGlobalScheduleConfiguration> ProjectGlobalScheduleConfigurations { get; set; }
        public DbSet<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }
        public DbSet<ProjectStep> ProjectSteps { get; set; }

        // Reference Data

        public DbSet<Currency> Currencies { get; set; }
    }
}