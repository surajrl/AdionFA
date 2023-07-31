using AdionFA.Infrastructure.Weka.Model;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Weka.Contracts
{
    public partial interface IWekaApiClient : IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Subscription credentials which uniquely identify client
        /// subscription.
        /// </summary>
        ServiceClientCredentials Credentials { get; }

        Task<HttpOperationResponse<IList<REPTreeOutputModel>>> GetREPTreeClassifierWithHttpMessagesAsync(
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
            CancellationToken cancellationToken = default);
    }
}
