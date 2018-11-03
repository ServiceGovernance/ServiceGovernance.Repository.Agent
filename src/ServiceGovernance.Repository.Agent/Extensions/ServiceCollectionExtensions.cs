using ServiceGovernance.Repository.Agent;
using ServiceGovernance.Repository.Agent.Configuration;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up the agent in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the repository agent services to the collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="setupBuilder">Delegate to define the configuration.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// services
        /// or
        /// setupBuilder
        /// </exception>
        public static IServiceRepositoryAgentBuilder AddServiceRepositoryAgent(this IServiceCollection services, Action<RepositoryAgentOptions> setupBuilder)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (setupBuilder == null)
                throw new ArgumentNullException(nameof(setupBuilder));

            var options = new RepositoryAgentOptions();
            setupBuilder(options);

            return AddServiceRepositoryAgent(services, options);
        }

        /// <summary>
        /// Adds the repository agent services to the collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="options">The agent options.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// services
        /// or
        /// options
        /// </exception>
        public static IServiceRepositoryAgentBuilder AddServiceRepositoryAgent(this IServiceCollection services, RepositoryAgentOptions options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.Validate();

            services.AddSingleton(options);
            services.AddSingleton<IServiceRepositoryClient, ServiceRepositoryClient>();

            services.AddHttpClient(ServiceRepositoryClient.HTTPCLIENT_NAME, client =>
            {
                client.BaseAddress = options.Repository;
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", $"ServiceRepositoryClient - {Assembly.GetExecutingAssembly().GetName().Version} - {options.ServiceIdentifier}");
            });

            return new ServiceRepositoryAgentBuilder(services);
        }
    }
}
