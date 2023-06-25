using AdionFA.Infrastructure.Common.Weka.Contracts;
using AdionFA.Infrastructure.Common.Weka.Model;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Common.Weka.Services
{
    public partial class WekaApiClient : ServiceClient<WekaApiClient>, IWekaApiClient
    {
        public Uri BaseUri { get; set; }

        public JsonSerializerSettings SerializationSettings { get; private set; }

        public JsonSerializerSettings DeserializationSettings { get; private set; }

        public ServiceClientCredentials Credentials { get; private set; }

        #region Methods

        public async Task<HttpOperationResponse<IList<REPTreeOutputModel>>> GetREPTreeClassifierWithHttpMessagesAsync(
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
            // Tracing

            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;

            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>
                {
                    { "path", path },
                    { "maxDepth", maxDepth },
                    { "numDecimalPlaces", numDecimalPlaces },
                    { "minSeed", minSeed },
                    { "maxSeed", maxSeed },
                    { "instances", instances },
                    { "ratio", ratio },
                    { "total", total },
                    { "isAssembly", isAssembly },
                    { "cancellationToken", cancellationToken }
                };

                ServiceClientTracing.Enter(_invocationId, this, "GetREPTreeClassifier", tracingParameters);
            }

            // Construct URL

            var _baseUrl = BaseUri.AbsoluteUri;
            var _url = new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "weka.reptree").ToString();
            var _queryParameters = new List<string>
            {
                string.Format("path={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(path, this.SerializationSettings).Trim('"')))
            };

            if (maxDepth != null)
            {
                _queryParameters.Add(string.Format("maxDepth={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(maxDepth, this.SerializationSettings).Trim('"'))));
            }
            if (numDecimalPlaces != null)
            {
                _queryParameters.Add(string.Format("numDecimalPlaces={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(numDecimalPlaces, SerializationSettings).Trim('"'))));
            }
            if (minSeed != null)
            {
                _queryParameters.Add(string.Format("minSeed={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(minSeed, SerializationSettings).Trim('"'))));
            }
            if (maxSeed != null)
            {
                _queryParameters.Add(string.Format("maxSeed={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(maxSeed, SerializationSettings).Trim('"'))));
            }
            if (instances != null)
            {
                _queryParameters.Add(string.Format("instances={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(instances, SerializationSettings).Trim('"'))));
            }
            if (ratio != null)
            {
                _queryParameters.Add(string.Format("ratio={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(ratio, SerializationSettings).Trim('"'))));
            }
            if (total != null)
            {
                _queryParameters.Add(string.Format("total={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(total, SerializationSettings).Trim('"'))));
            }
            if (isAssembly != null)
            {
                _queryParameters.Add(string.Format("isAssembly={0}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(isAssembly, SerializationSettings).Trim('"'))));
            }

            if (_queryParameters.Count > 0)
            {
                _url += "?" + string.Join("&", _queryParameters);
            }

            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new Uri(_url);

            // Set Headers
            if (customHeaders != null)
            {
                foreach (var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;

            // Set Credentials
            if (this.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await this.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            }

            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }

            cancellationToken.ThrowIfCancellationRequested();

            _httpResponse = await this.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);

            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }

            HttpStatusCode _statusCode = _httpResponse.StatusCode;

            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;

            if ((int)_statusCode != 200)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }

            // Create Result
            var _result = new HttpOperationResponse<IList<REPTreeOutputModel>>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<IList<REPTreeOutputModel>>(_responseContent, this.DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        #endregion Methods

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public WekaApiClient(params DelegatingHandler[] handlers) : base(handlers)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public WekaApiClient(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected WekaApiClient(Uri baseUri, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this.BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected WekaApiClient(Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this.BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public WekaApiClient(ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public WekaApiClient(ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public WekaApiClient(Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this.BaseUri = baseUri;
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the WekaApiClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public WekaApiClient(Uri baseUri, ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this.BaseUri = baseUri;
            this.Credentials = credentials;
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        ///</summary>
        partial void CustomInitialize();

        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            BaseUri = new Uri("http://localhost:8080");
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            CustomInitialize();
        }
    }
}
