﻿using AdionFA.Domain.Entities;
using AdionFA.Domain.Entities.Configuration;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
            //            optionsBuilder.LogTo(
            //                IoC.Kernel.Get<ILogger>().Information,
            //                options: DbContextLoggerOptions.None)
            //                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity configuration

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var methodInfo = typeof(EnumExtension).GetMethod("GetMetadata");

            // Setting

            foreach (var setting in Enum.GetValues(typeof(SettingEnum)))
            {
                var meta = (Metadata)methodInfo.Invoke(setting, new object[] { setting });
                modelBuilder.Entity<Setting>().HasData(
                    new Setting
                    {
                        SettingId = (int)setting,

                        // Reference data base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,

                        // Entity base

                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false,
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

                        // Reference data base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,

                        // Entity base

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

                        // Reference data base

                        Code = meta.Code,
                        Name = meta.Name,
                        Value = meta.Value,

                        // Entity base

                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false,
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

                        // Reference data base

                        Code = meta.Code,
                        Name = meta.Name,

                        // Entity base

                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false,
                    }
                );
            }

            // Market region

            foreach (var mr in Enum.GetValues(typeof(MarketRegionEnum)))
            {
                var meta = (Metadata)methodInfo.Invoke(mr, new object[] { mr });
                modelBuilder.Entity<MarketRegion>().HasData(
                    new MarketRegion
                    {
                        MarketRegionId = (int)mr,

                        // Reference data base

                        Code = meta.Code,
                        Name = meta.Name,

                        // Entity base

                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false,
                    }
                );
            }

            // Global configuration

            modelBuilder.Entity<GlobalConfiguration>().HasData(new GlobalConfiguration
            {
                GlobalConfigurationId = 1,

                // Period

                FromDateIS = null,
                ToDateIS = null,

                FromDateOS = null,
                ToDateOS = null,

                // Schedule 

                WithoutSchedule = true,

                // MetaTrader

                ExpertAdvisorHost = "192.168.1.35",
                ExpertAdvisorPublisherPort = "5551",
                ExpertAdvisorResponsePort = "5550",

                // Extractor

                ExtractorMinVariation = 50,

                // Weka

                TotalDecimalWeka = 5,
                MinimalSeed = 100,
                MaximumSeed = 1000000,

                // Builder

                IsProgressiveness = false,
                MaxProgressivenessVariation = (decimal)2.0,

                MaxCorrelationPercent = (decimal)2.0,

                MaxParallelism = 1,

                NodeBuilderConfigurationId = 1,
                AssemblyBuilderConfigurationId = 1,
                CrossingBuilderConfigurationId = 1,

                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            });

            modelBuilder.Entity<NodeBuilderConfiguration>().HasData(new NodeBuilderConfiguration
            {
                NodeBuilderConfigurationId = 1,

                NodesUPTarget = 6,
                NodesDOWNTarget = 6,
                TotalTradesTarget = 100,

                MinTotalTradesIS = 200,
                MinSuccessRatePercentIS = (decimal)40.0,

                MinTotalTradesOS = 100,
                MinSuccessRatePercentOS = (decimal)40.0,

                MaxSuccessRateVariation = (decimal)5.0,
                WekaNTotal = 300,
                WekaStartDepth = 4,
                WekaEndDepth = 12,
                WekaMaxRatio = (decimal)1.5,

                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            });

            modelBuilder.Entity<AssemblyBuilderConfiguration>().HasData(new AssemblyBuilderConfiguration
            {
                AssemblyBuilderConfigurationId = 1,

                AssemblyNodesUPTarget = 6,
                AssemblyNodesDOWNTarget = 6,
                TotalTradesTarget = 1000,

                MinTotalTradesIS = 100,

                MinSuccessRateImprovementIS = (decimal)2.0,
                MinSuccessRateImprovementOS = (decimal)2.0,

                MaxSuccessRateImprovementIS = (decimal)4.0,
                MaxSuccessRateImprovementOS = (decimal)4.0,

                WekaNTotal = 300,
                WekaStartDepth = 1,
                WekaEndDepth = 6,
                WekaMaxRatio = (decimal)1.5,

                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            });

            modelBuilder.Entity<CrossingBuilderConfiguration>().HasData(new CrossingBuilderConfiguration
            {
                CrossingBuilderConfigurationId = 1,

                StrategyNodesUPTarget = 6,
                StrategyNodesDOWNTarget = 6,
                TotalTradesTarget = 1000,

                MinSuccessRateImprovementIS = (decimal)2.0,
                MinSuccessRateImprovementOS = (decimal)2.0,

                MaxSuccessRateImprovementIS = (decimal)4.0,
                MaxSuccessRateImprovementOS = (decimal)4.0,

                WekaNTotal = 300,
                WekaStartDepth = 1,
                WekaEndDepth = 6,
                WekaMaxRatio = (decimal)1.5,

                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            });

            // Schedule configuration

            modelBuilder.Entity<GlobalScheduleConfiguration>().HasData(
                new GlobalScheduleConfiguration
                {
                    GlobalScheduleConfigurationId = 1,

                    // Global configuration

                    GlobalConfigurationId = 1,

                    // Market region

                    MarketRegionId = (int)MarketRegionEnum.America,

                    FromTimeInSeconds = 54000,
                    ToTimeInSeconds = 82800,

                    // Entity base

                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new GlobalScheduleConfiguration
                {
                    GlobalScheduleConfigurationId = 2,

                    // Configuration

                    GlobalConfigurationId = 1,

                    // Market region

                    MarketRegionId = (int)MarketRegionEnum.Europe,

                    FromTimeInSeconds = 32400,
                    ToTimeInSeconds = 64800,

                    // Entity base

                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new GlobalScheduleConfiguration
                {
                    GlobalScheduleConfigurationId = 3,

                    // Configuration

                    GlobalConfigurationId = 1,

                    // Market region

                    MarketRegionId = (int)MarketRegionEnum.Asia,
                    FromTimeInSeconds = 3600,
                    ToTimeInSeconds = 32400,

                    // Entity base

                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                }
            );
        }

        // Entities

        // Common

        public DbSet<NodeBuilderConfiguration> NodeBuilderConfiguration { get; set; }
        public DbSet<AssemblyBuilderConfiguration> AssemblyBuilderConfiguration { get; set; }
        public DbSet<CrossingBuilderConfiguration> CrossingBuilderConfiguration { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<GlobalConfiguration> GlobalConfigurations { get; set; }
        public DbSet<GlobalScheduleConfiguration> GlobalScheduleConfigurations { get; set; }

        // Market Data

        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<MarketRegion> MarketRegions { get; set; }
        public DbSet<HistoricalData> HistoricalDatas { get; set; }
        public DbSet<HistoricalDataCandle> HistoricalDataCandles { get; set; }


        // Project

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }
        public DbSet<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }
    }
}