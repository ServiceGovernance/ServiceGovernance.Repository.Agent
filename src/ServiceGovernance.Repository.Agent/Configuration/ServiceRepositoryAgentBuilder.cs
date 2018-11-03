using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceGovernance.Repository.Agent.Configuration
{
    /// <summary>
    /// Service repository agent helper class for DI configuration
    /// </summary>
    public class ServiceRepositoryAgentBuilder : IServiceRepositoryAgentBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepositoryAgentBuilder"/> class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <exception cref="System.ArgumentNullException">services</exception>
        public ServiceRepositoryAgentBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
