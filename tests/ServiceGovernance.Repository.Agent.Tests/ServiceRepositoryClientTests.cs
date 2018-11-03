using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using ServiceGovernance.Repository.Agent.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Testing.HttpClient;

namespace ServiceGovernance.Repository.Agent.Tests
{
    [TestFixture]
    public class ServiceRepositoryClientTests
    {
        protected ServiceRepositoryClient _repositoryClient;
        protected RepositoryAgentOptions _options;
        protected Mock<IHttpClientFactory> _httpClientFactory;
        protected HttpClientTestingFactory _httpClientTestingFactory;
        protected Mock<IApiDescriptionProvider> _apiDescriptionProvider;

        [SetUp]
        public void Setup()
        {
            _options = new RepositoryAgentOptions();
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _apiDescriptionProvider = new Mock<IApiDescriptionProvider>();

            _repositoryClient = new ServiceRepositoryClient(_options, _httpClientFactory.Object, new Mock<ILogger<ServiceRepositoryClient>>().Object, _apiDescriptionProvider.Object);

            _httpClientTestingFactory = new HttpClientTestingFactory();
            _httpClientTestingFactory.HttpClient.BaseAddress = new Uri("http://repository.com");
            _httpClientFactory.Setup(f => f.CreateClient(ServiceRepositoryClient.HTTPCLIENT_NAME)).Returns(_httpClientTestingFactory.HttpClient);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClientTestingFactory.EnsureNoOutstandingRequests();
        }

        public class PublishServiceApiMethod : ServiceRepositoryClientTests
        {
            [Test]
            public async Task Calls_Endpoint_With_ApiDocument()
            {
                _options.ServiceIdentifier = "api1";
                _apiDescriptionProvider.Setup(p => p.GetDescriptions()).Returns(new OpenApiDocument { Info = new OpenApiInfo { Title = "My Api" } });

                var action = Task.Run(() => _repositoryClient.PublishServiceApi());

                var request = _httpClientTestingFactory.Expect(HttpMethod.Post, "http://repository.com/v1/api/" + _options.ServiceIdentifier);
                var document = JsonConvert.DeserializeObject<OpenApiDocument>(request.Request.Content.ReadAsStringAsync().Result);
                document.Info.Title.Should().Be("My Api");

                request.Respond(HttpStatusCode.OK);
                await action;
            }
        }
    }
}
