﻿using AdionFA.Infrastructure.Weka.Contracts;
using AdionFA.Infrastructure.Weka.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Weka.Services
{
    public static partial class WekaApiClientExtensions
    {
        public static IList<REPTreeOutputModel> GetREPTreeClassifier(
            this IWekaApiClient operations,
            string path,
            int? maxDepth = default,
            int? numDecimalPlaces = default,
            int? minSeed = default,
            int? maxSeed = default,
            int? instances = default,
            double? ratio = default,
            double? total = default,
            bool? isAssembly = default)
        {
            return Task.Factory.StartNew(s => ((IWekaApiClient)s).GetREPTreeClassifierAsync(
                path,
                maxDepth,
                numDecimalPlaces,
                minSeed,
                maxSeed,
                instances,
                ratio,
                total,
                isAssembly),
                operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }

        public static async Task<IList<REPTreeOutputModel>> GetREPTreeClassifierAsync(
            this IWekaApiClient operations,
            string path,
            int? maxDepth = default,
            int? numDecimalPlaces = default,
            int? minSeed = default,
            int? maxSeed = default,
            int? instances = default,
            double? ratio = default,
            double? total = default,
            bool? isAssembly = default,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default)
        {
            using var _result = await operations.GetREPTreeClassifierWithHttpMessagesAsync(
                path,
                maxDepth,
                numDecimalPlaces,
                minSeed,
                maxSeed,
                instances,
                ratio,
                total,
                isAssembly,
                null,
                cancellationToken).ConfigureAwait(false);

            return _result.Body;
        }
    }
}
