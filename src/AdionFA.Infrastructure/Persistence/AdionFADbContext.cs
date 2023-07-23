using AdionFA.Domain.Entities;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Enums.Market;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Model;
using AdionFA.Infrastructure.Managements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace AdionFA.Infrastructure.Persistence
{
    public class AdionFADbContext : DbContext
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity Configuration

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var userId = "0";
            var username = "admin";

            var methodInfo = typeof(EnumExtension).GetMethod("GetMetadata");

            // Setting

            foreach (var setting in Enum.GetValues(typeof(SettingEnum)))
            {
                var meta = (Metadata)methodInfo.Invoke(setting, new object[] { setting });
                modelBuilder.Entity<Setting>().HasData(
                    new Setting
                    {
                        SettingId = (int)setting,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
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

            foreach (var symbol in Enum.GetValues(typeof(SymbolEnum)))
            {
                var meta = (Metadata)methodInfo.Invoke(symbol, new object[] { symbol });
                modelBuilder.Entity<Symbol>().HasData(
                    new Symbol
                    {
                        SymbolId = (int)symbol,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,

                        // Entity Base

                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,

                        IsDeleted = false,
                    }
                );
            }

            // Timeframe

            foreach (var timeframe in Enum.GetValues(typeof(TimeframeEnum)))
            {
                var meta = (Metadata)methodInfo.Invoke(timeframe, new object[] { timeframe });
                modelBuilder.Entity<Timeframe>().HasData(
                    new Timeframe
                    {
                        TimeframeId = (int)timeframe,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,

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
                var meta = (Metadata)methodInfo.Invoke(market, new object[] { market });
                modelBuilder.Entity<Market>().HasData(
                    new Market
                    {
                        MarketId = (int)market,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,

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
                var meta = (Metadata)methodInfo.Invoke(mr, new object[] { mr });
                modelBuilder.Entity<MarketRegion>().HasData(
                    new MarketRegion
                    {
                        MarketRegionId = (int)mr,

                        // Reference Data Base

                        Code = meta.Code,
                        Name = meta.Name,

                        // Entity Base

                        IsDeleted = false,
                        CreatedById = userId,
                        CreatedByUserName = username,
                        CreatedOn = DateTime.UtcNow,
                    }
                );
            }

            // Global Configuration

            modelBuilder.Entity<GlobalConfiguration>().HasData(new GlobalConfiguration
            {
                GlobalConfigurationId = 1,

                // Period

                FromDateIS = null,
                ToDateIS = null,

                FromDateOS = null,
                ToDateOS = null,

                WithoutSchedule = true,

                // Extractor

                ExtractorMinVariation = 50,

                // MetaTrader

                ExpertAdvisorHost = null,
                ExpertAdvisorPublisherPort = null,
                ExpertAdvisorResponsePort = null,

                // Weka

                TotalInstanceWeka = 1,
                DepthWeka = 6,
                TotalDecimalWeka = 5,
                MinimalSeed = 100,
                MaximumSeed = 1000000,
                MaxRatioTree = (decimal)1.5,
                NTotalTree = 300,

                // Strategy Builder

                SBMinTotalTradesIS = 300,
                SBMinSuccessRatePercentIS = 55,

                SBMinTotalTradesOS = 100,
                SBMinSuccessRatePercentOS = 55,

                SBMaxSuccessRateVariation = 4,

                MaxProgressivenessVariation = 2,
                IsProgressiveness = false,

                SBMaxCorrelationPercent = 2,

                SBWinningStrategyDOWNTarget = 6,
                SBWinningStrategyUPTarget = 6,
                SBTotalTradesTarget = 300,

                // Assembly Builder

                ABMinTotalTradesIS = 300,
                ABMinImprovePercent = 5,
                ABWekaMaxRatioTree = (decimal)1.5,
                ABWekaNTotalTree = 300,

                // Entity Base

                CreatedById = userId,
                CreatedByUserName = username,
                CreatedOn = DateTime.UtcNow,

                IsDeleted = false,
            });

            // Schedule Configuration

            modelBuilder.Entity<GlobalScheduleConfiguration>().HasData(
                new GlobalScheduleConfiguration
                {
                    GlobalScheduleConfigurationId = 1,

                    // Configuration

                    GlobalConfigurationId = 1,

                    // Market Region

                    MarketRegionId = (int)MarketRegionEnum.America,

                    FromTimeInSeconds = 54000,
                    ToTimeInSeconds = 82800,

                    // Entity Base

                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,

                    IsDeleted = false,
                },
                new GlobalScheduleConfiguration
                {
                    GlobalScheduleConfigurationId = 2,

                    // Configuration

                    GlobalConfigurationId = 1,

                    // Market Region

                    MarketRegionId = (int)MarketRegionEnum.Europe,

                    FromTimeInSeconds = 32400,
                    ToTimeInSeconds = 64800,

                    // Entity Base

                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,

                    IsDeleted = false,
                },
                new GlobalScheduleConfiguration
                {
                    GlobalScheduleConfigurationId = 3,

                    // Configuration

                    GlobalConfigurationId = 1,

                    // Market Region

                    MarketRegionId = (int)MarketRegionEnum.Asia,
                    FromTimeInSeconds = 3600,
                    ToTimeInSeconds = 32400,

                    // Entity Base

                    CreatedById = userId,
                    CreatedByUserName = username,
                    CreatedOn = DateTime.UtcNow,

                    IsDeleted = false,
                }
            );
        }

        // Entities

        // Common

        public DbSet<Setting> Settings { get; set; }
        public DbSet<GlobalConfiguration> Configurations { get; set; }
        public DbSet<GlobalScheduleConfiguration> ScheduleConfigurations { get; set; }

        // Market Data

        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<MarketRegion> MarketRegions { get; set; }
        public DbSet<HistoricalData> HistoricalDatas { get; set; }
        public DbSet<HistoricalDataCandle> HistoricalDataCandles { get; set; }


        // Project

        public DbSet<Project> Projects { get; set; }
    }
}