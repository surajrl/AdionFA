using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Core.Data.Persistence;
using AdionFA.Infrastructure.Core.Data.Repositories;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ControlzEx.Controls;
using Ninject.Extensions.Conventions.Syntax;
using System.Data;
using System.Linq.Expressions;
using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Contracts.Bases;

using AdionFA.Core.Domain.Extensions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Reflection;

namespace AdionFA.Benchmark
{
    [MemoryDiagnoser]
    public class BacktestBenchmark
    {
        public static void Main(string[] args)
        {
            //BenchmarkRunner.Run<BacktestBenchmark>();
            BacktestExecutionUsingIEnumerable();
        }

        private static AdionFADbContext _db = new AdionFADbContext();

        [Benchmark]
        public static void BacktestExecutionUsingIEnumerable()
        {
            Expression<Func<HistoricalData, bool>> predicate = hd => hd.HistoricalDataId == 1 && (hd.EndDate ?? DateTime.MinValue) == DateTime.MinValue;
            Expression<Func<HistoricalData, dynamic>> includes = hd => hd.HistoricalDataCandles;

            var hd = FirstOrDefault(predicate, hd => hd.HistoricalDataCandles);

            return;
        }

        [Benchmark]
        public void BacktestExecutionUsingIList()
        {
            var allCandles = new List<Candle>();
        }

        public static IQueryable<HistoricalData> GetAll()
        {
            IQueryable<HistoricalData> query = _db.Set<HistoricalData>().Where(e => e.IsDeleted == false);
            return query;
        }

        public static IQueryable<HistoricalData> GetAll(params string[] include)
        {
            IQueryable<HistoricalData> query = _db.Set<HistoricalData>().Where(e => e.IsDeleted == false);
            if (include != null && include.Any())
            {
                query = include.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public static IQueryable<HistoricalData> GetAll(params Expression<Func<HistoricalData, dynamic>>[] includes)
        {
            IQueryable<HistoricalData> query = GetAll();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public static IQueryable<HistoricalData> GetAll(Expression<Func<HistoricalData, bool>> predicate)
        {
            IQueryable<HistoricalData> query = GetAll();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public static IQueryable<HistoricalData> GetAll(Expression<Func<HistoricalData, bool>> predicate, params Expression<Func<HistoricalData, dynamic>>[] includes)
        {
            IQueryable<HistoricalData> query = GetAll(predicate);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public static IQueryable<HistoricalData> GetAll(Expression<Func<HistoricalData, bool>> predicate, params string[] includes)
        {
            IQueryable<HistoricalData> query = GetAll(predicate);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        // FirstOrDefault

        public static HistoricalData FirstOrDefault()
        {
            IQueryable<HistoricalData> query = GetAll();
            return query.FirstOrDefault();
        }

        public static HistoricalData FirstOrDefault(Expression<Func<HistoricalData, bool>> predicate)
        {
            IQueryable<HistoricalData> query = GetAll(predicate);
            return query.FirstOrDefault();
        }

        public static HistoricalData FirstOrDefault(params Expression<Func<HistoricalData, dynamic>>[] includes)
        {
            IQueryable<HistoricalData> query = GetAll(includes);
            return query.FirstOrDefault();
        }

        public static HistoricalData FirstOrDefault(Expression<Func<HistoricalData, bool>> predicate, params Expression<Func<HistoricalData, dynamic>>[] includes)
        {
            IQueryable<HistoricalData> query = GetAll(predicate, includes);
            return query.FirstOrDefault();
        }

        public static HistoricalData FirstOrDefault(Expression<Func<HistoricalData, bool>> predicate, params string[] includes)
        {
            IQueryable<HistoricalData> query = GetAll(predicate, includes);
            return query.FirstOrDefault();
        }
    }
}