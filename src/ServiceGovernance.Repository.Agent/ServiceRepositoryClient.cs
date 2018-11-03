using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using ServiceGovernance.Repository.Agent.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

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
        private readonly IApiDescriptionProvider _apiDescriptionProvider;

        internal const string HTTPCLIENT_NAME = "ServiceRepositoryHttpClient";

        public ServiceRepositoryClient(RepositoryAgentOptions options, IHttpClientFactory httpClientFactory, ILogger<ServiceRepositoryClient> logger, IApiDescriptionProvider apiDescriptionProvider)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiDescriptionProvider = apiDescriptionProvider ?? throw new ArgumentNullException(nameof(apiDescriptionProvider));
        }

        /// <summary>
        /// Publishes the API to the service repository
        /// </summary>
        /// <returns></returns>
        public void PublishServiceApi()
        {
            var document = _apiDescriptionProvider.GetDescriptions();

            _logger.LogDebug("Pushing service api description");

            var content = GetDocumentAsHttpContent(document);
            var client = _httpClientFactory.CreateClient(HTTPCLIENT_NAME);

            try
            {               
                var response = client.PostAsync("v1/api/" + Uri.EscapeUriString(_options.ServiceIdentifier), content).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                _logger.LogInformation($"Service Api pushing in repository as '{_options.ServiceIdentifier}' was successfull.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Service Api pushing failed: {ex.Message}");
                throw;
            }
        }

        private HttpContent GetDocumentAsHttpContent(OpenApiDocument document)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false, true), 1024, true))
            {
                var apiWriter = new OpenApiJsonWriter(writer);
                document.SerializeAsV3(apiWriter);
                apiWriter.Flush();
                writer.Flush();                
            }

            stream.Position = 0;
            return new StreamContent(stream);
        }
    }
}
