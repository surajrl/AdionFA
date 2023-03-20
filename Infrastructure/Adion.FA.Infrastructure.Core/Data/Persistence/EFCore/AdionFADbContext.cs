using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Aggregates.Market;
using Adion.FA.Core.Domain.Aggregates.MetaTrader;
using Adion.FA.Core.Domain.Aggregates.Organization;
using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Core.Domain.Aggregates.ReferenceData;
using Adion.FA.Infrastructure.Common.Managements;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adion.FA.Infrastructure.Core.Data.Persistence
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
            #region EntityConfiguration

            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> types = assembly.GetTypes().Where(
                t => !t.IsInterface &&
                     typeof(IAdionFAETC).IsAssignableFrom(t)).ToList();
            types.ForEach(t =>
            {
                var genericType = t.GetInterface(typeof(IEntityTypeConfiguration<>).FullName).GetGenericArguments().Single();
                var entityConfiguration = assembly.CreateInstance(t.FullName);
                MethodInfo m = typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.ApplyConfiguration));
                m.MakeGenericMethod(genericType).Invoke(modelBuilder, new object[] { entityConfiguration });
            });

            #endregion

            #region Seed

            MethodInfo m = typeof(EnumExtension).GetMethod("GetMetadata");

            string userId = "11111111-1111-1111-11111111111111111";
            string username = "sysadmin";
            string tenantId = "22222222-2222-2222-2222-222222222222"; 

            #region Common

            //EntityType
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

            //Setting
            foreach (var setting in Enum.GetValues(typeof(SettingEnum)))
            {
                var meta = (Metadata)m.Invoke(setting, new object[] { setting });
                modelBuilder.Entity<Setting>().HasData(
                    new Setting
                    {
                        SettingId = (int)setting,
                        Key = meta.Code,
                        Value = meta.Name,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            #endregion

            #region Market
            
            //Currency Pair
            foreach (var cp in Enum.GetValues(typeof(CurrencyPairEnum)))
            {
                var meta = (Metadata)m.Invoke(cp, new object[] { cp });
                modelBuilder.Entity<CurrencyPair>().HasData(
                    new CurrencyPair
                    {
                        CurrencyPairId = (int)cp,
                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,

                        CurrencyFromId = (int)Enum.Parse(typeof(CurrencyEnum), meta.Code.Split("-")[0]),
                        CurrencyToId = (int)Enum.Parse(typeof(CurrencyEnum), meta.Code.Split("-")[1]),

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            //Currency Period
            foreach (var cp in Enum.GetValues(typeof(CurrencyPeriodEnum)))
            {
                var meta = (Metadata)m.Invoke(cp, new object[] { cp });
                modelBuilder.Entity<CurrencyPeriod>().HasData(
                    new CurrencyPeriod
                    {
                        CurrencyPeriodId = (int)cp,
                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,
                        Value = meta.Value,
                        Symbol = meta.Symbol,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }

            //Currency Spread
            foreach (var csp in Enum.GetValues(typeof(CurrencySpreadEnum)))
            {
                var meta = (Metadata)m.Invoke(csp, new object[] { csp });
                modelBuilder.Entity<CurrencySpread>().HasData(
                    new CurrencySpread
                    {
                        CurrencySpreadId = (int)csp,
                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,
                        Value = meta.Value,
                        Symbol = meta.Symbol,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }
            
            //Market
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

            //MarketRegion
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

            #endregion

            #region Organization

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

            #endregion

            #region Project

            //GeneralConfiguration
            modelBuilder.Entity<ProjectGlobalConfiguration>().HasData(new ProjectGlobalConfiguration
            {
                Description = "Default Configuration",
                ProjectGlobalConfigurationId = 1,

                #region Extractor
                Variation = 50,
                #endregion

                #region Period
                FromDateIS = null,
                ToDateIS = null,
                
                FromDateOS = null,
                ToDateOS = null,
                #endregion

                #region Schedule
                WithoutSchedule = true,
                #endregion

                #region Currency
                CurrencyPairId = (int)CurrencyPairEnum.EURUSD,
                CurrencyPeriodId = (int)CurrencyPeriodEnum.H1,
                CurrencySpreadId = (int)CurrencySpreadEnum.Five,
                #endregion

                #region Weka
                TotalInstanceWeka = 1,

                DepthWeka = 10,
                MinAdjustDepthWeka = 5,

                TotalDecimalWeka = 8,
                MinimalSeed = 100,
                MaximumSeed = 1000000,

                MaxRatioTree = (decimal)1.5,
                MinAdjustMaxRatioTree = 1,
                NTotalTree = 300,
                MinAdjustNTotalTree = 150,
                #endregion

                #region Strategy Builder
                MinTransactionCountIS = 600,
                MinAdjustMinTransactionCountIS = 360,
                MinPercentSuccessIS = 55,
                MinAdjustMinPercentSuccessIS = 50,

                MinTransactionCountOS = 360,
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
                AsynchronousMode = false,
                #endregion

                #region Assembled Builder
                TransactionTarget = 600,
                MinAssemblyPercent = 5,
                TotalAssemblyIterations = 1,
                #endregion

                StartDate = DateTime.UtcNow,
                EndDate = null,

                IsDeleted = false,
                Inaccesible = false,
                TenantId = tenantId,
                CreatedById = userId,
                CreatedOn = DateTime.UtcNow,
                CreatedByUserName = username
            });

            //ScheduleConfiguration
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

            //ProjectStep
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

            #endregion

            #region Data Reference

            //Currencies
            foreach (var c in Enum.GetValues(typeof(CurrencyEnum)))
            {
                var meta = (Metadata)m.Invoke(c, new object[] { c });
                modelBuilder.Entity<Currency>().HasData(
                    new Currency 
                    { 
                        CurrencyId = (int)c,
                        Code = meta.Code,
                        Name = meta.Name,
                        Description = meta.Description,
                        Value = meta.Value,
                        Symbol = meta.Symbol,

                        IsDeleted = false,
                        Inaccesible = false,
                        TenantId = tenantId,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserName = username
                    }
                );
            }
            
            #endregion

            #endregion
        }

        #region Common
        public DbSet<EntityServiceHost> EntityServiceHosts { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        #endregion

        #region MarketData
        public DbSet<CurrencyPair> CurrencyPairs { get; set; }
        public DbSet<CurrencyPeriod> CurrencyPeriods { get; set; }
        public DbSet<CurrencySpread> CurrencySpreads { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<MarketData> MarketDatas { get; set; }
        public DbSet<MarketDataDetail> MarketDataDetails { get; set; }
        public DbSet<MarketRegion> MarketRegions { get; set; }
        #endregion

        #region MetaTrader
        public DbSet<ExpertAdvisor> ExpertAdvisors { get; set; }
        #endregion

        #region Project
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }
        public DbSet<ProjectGlobalConfiguration> ProjectGlobalConfigurations { get; set; }
        public DbSet<ProjectGlobalScheduleConfiguration> ProjectGlobalScheduleConfigurations { get; set; }
        public DbSet<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }
        public DbSet<ProjectStep> ProjectSteps { get; set; }
        #endregion

        #region Reference Data
        public DbSet<Currency> Currencies { get; set; }
        #endregion
    }
}
