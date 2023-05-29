using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Aggregates.MetaTrader;
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

            var userId = "0000";
            var username = "admin";

            // Entity Type

            foreach (var et in Enum.GetValues(typeof(EntityTypeEnum)))
            {
                var meta = (Metadata)m.Invoke(et, new object[] { et });
                modelBuilder.Entity<EntityType>().HasData(
                    new EntityType
                    {
                        EntityTypeId = (int)et,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow
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

                        // Reference Data Base

                        Code = meta.Code,
                        Value = meta.Value,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,
                    }
                );
            }

            // Symbol

            modelBuilder.Entity<Symbol>().HasData(
                new Symbol
                {
                    SymbolId = 1,

                    // Reference Data Base

                    Code = "EURUSD",
                    Name = "EURUSD",
                    Description = "Euro vs US Dollar",

                    // Entity Base

                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,
                });

            // Timeframe

            foreach (var timeframe in Enum.GetValues(typeof(TimeframeEnum)))
            {
                var meta = (Metadata)m.Invoke(timeframe, new object[] { timeframe });
                modelBuilder.Entity<Timeframe>().HasData(
                    new Timeframe
                    {
                        TimeframeId = (int)timeframe,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,
                        Description = meta.Description,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,
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

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,
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

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,
                    }
                );
            }

            // Configuration

            modelBuilder.Entity<Configuration>().HasData(new Configuration
            {
                ConfigurationId = 1,

                // Period

                FromDateIS = null,
                ToDateIS = null,

                FromDateOS = null,
                ToDateOS = null,

                WithoutSchedule = true,

                // Historical Data Information

                SymbolId = 1,
                TimeframeId = (int)TimeframeEnum.H1,

                // Extractor

                ExtractorMinVariation = 50,

                // Weka

                TotalInstanceWeka = 1,
                DepthWeka = 6,
                TotalDecimalWeka = 5,
                MinimalSeed = 100,
                MaximumSeed = 1000000,
                MaxRatioTree = (decimal)1.5,
                NTotalTree = 300,

                // Strategy Builder

                SBMinTransactionsIS = 300,
                SBMinPercentSuccessIS = 55,

                SBMinTransactionsOS = 100,
                SBMinPercentSuccessOS = 55,

                SBMaxTransactionsVariation = 4,

                Progressiveness = 2,
                IsProgressiveness = false,

                SBMaxPercentCorrelation = 2,

                SBWinningStrategyDOWNTarget = 6,
                SBWinningStrategyUPTarget = 6,
                SBTransactionsTarget = 300,

                // Assembled Builder

                ABTransactionsTarget = 600,
                ABMinImprovePercent = 5,

                // Time Sensitive Base

                StartDate = DateTime.UtcNow,
                EndDate = null,
                Description = "Default Configuration",

                // Entity Base

                IsDeleted = false,
                CreatedById = userId,
                CreatedByUserName = username,
                CreatedOn = DateTime.UtcNow,
            });

            // Schedule Configuration

            modelBuilder.Entity<ScheduleConfiguration>().HasData(
                new ScheduleConfiguration
                {
                    ScheduleConfigurationId = 1,

                    // Configuration

                    ConfigurationId = 1,

                    // Market Region

                    MarketRegionId = (int)MarketRegionEnum.America,

                    FromTimeInSeconds = 54000,
                    ToTimeInSeconds = 82800,

                    // Time Sensitive Base

                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    Description = "Default Schedule America",

                    // Entity Base

                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,
                },
                new ScheduleConfiguration
                {
                    ScheduleConfigurationId = 2,

                    // Configuration

                    ConfigurationId = 1,

                    // Market Region

                    MarketRegionId = (int)MarketRegionEnum.Europe,

                    FromTimeInSeconds = 32400,
                    ToTimeInSeconds = 64800,

                    // Time Sensitive Base

                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    Description = "Default Schedule Europe",

                    // Entity Base

                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,
                },
                new ScheduleConfiguration
                {
                    ScheduleConfigurationId = 3,

                    // Configuration

                    ConfigurationId = 1,

                    // Market Region

                    MarketRegionId = (int)MarketRegionEnum.Asia,

                    FromTimeInSeconds = 3600,
                    ToTimeInSeconds = 32400,

                    // Time Sensitive Base

                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    Description = "Default Schedule Asia",

                    // Entity Base

                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,
                }
            );

            // Currencies

            foreach (var c in Enum.GetValues(typeof(CurrencyEnum)))
            {
                var meta = (Metadata)m.Invoke(c, new object[] { c });
                modelBuilder.Entity<Currency>().HasData(
                    new Currency
                    {
                        CurrencyId = (int)c,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,
                        Description = meta.Description,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,
                    }
                );
            }
        }

        // Common

        public DbSet<EntityServiceHost> EntityServiceHosts { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<ScheduleConfiguration> ScheduleConfigurations { get; set; }

        // Market Data

        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<HistoricalData> HistoricalDatas { get; set; }
        public DbSet<HistoricalDataCandle> HistoricalDataCandles { get; set; }
        public DbSet<MarketRegion> MarketRegions { get; set; }

        // MetaTrader

        public DbSet<ExpertAdvisor> ExpertAdvisors { get; set; }

        // Project

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }
        public DbSet<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }

        // Reference Data

        public DbSet<Currency> Currencies { get; set; }
    }
}