using Microsoft.Extensions.Logging;
using ServiceGovernance.Repository.Agent.Configuration;
using System;
using System.Net.Http;

namespace ServiceGovernance.Repository.Agent
{
    /// <summary>
    /// The service repository client implementation
    /// </summary>
    public class ServiceRepositoryClient : IServiceRepositoryClient
    {
        private readonly RepositoryAgentOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ServiceRepositoryClient> _logger;

        internal const string HTTPCLIENT_NAME = "ServiceRepositoryHttpClient";

        public ServiceRepositoryClient(RepositoryAgentOptions options, IHttpClientFactory httpClientFactory, ILogger<ServiceRepositoryClient> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
