﻿using Adion.FA.Core.Domain.Aggregates.Core;
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

namespace Adion.FA.Infrastructure.Core.Data.Persistence.EFCore
{
    public class AdionSecurityDbContext : DbContext, IAdionSecurityDbContext
    {
        private readonly string _connectionString;

        public AdionSecurityDbContext()
        {
            _connectionString = AppSettingsManager.Instance.Get<AppSettings>().SecurityConnection;
        }

        public AdionSecurityDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);

#if DEBUG
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(b => b.AddDebug()));
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region EntityConfiguration
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> types = assembly.GetTypes().Where(
                t => !t.IsInterface &&
                     typeof(IAdionSecurityETC).IsAssignableFrom(t)).ToList();
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

            #region Core

            //CoreUserType
            foreach (var cut in Enum.GetValues(typeof(CoreEnum.UserType)))
            {
                var meta = (Metadata)m.Invoke(cut, new object[] { cut });
                modelBuilder.Entity<CoreUserType>().HasData(
                    new CoreUserType
                    {
                        UserTypeId = (int)cut,
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

            //CoreUser
            modelBuilder.Entity<CoreUser>().HasData(
                new CoreUser
                {
                    UserId = userId,
                    UserTypeId = (int)CoreEnum.UserType.Employee,
                    Email = "admin@adionteam.com",
                    UserName = username,
                    Password = "Pa$$W0rd",

                    IsDeleted = false,
                    Inaccesible = false,
                    TenantId = tenantId,
                    CreatedById = userId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByUserName = username
                }
            );

            #endregion

            #endregion
        }

        #region Core
        public DbSet<CoreUser> CoreUsers { get; set; }
        public DbSet<CoreUserType> CoreUserTypes { get; set; }
        #endregion
    }
}
