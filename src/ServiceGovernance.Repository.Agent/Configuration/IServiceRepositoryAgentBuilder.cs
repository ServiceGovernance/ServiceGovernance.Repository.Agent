using Microsoft.Extensions.DependencyInjection;

namespace ServiceGovernance.Repository.Agent.Configuration
{
    /// <summary>
    /// Service registry agent builder interface
    /// </summary>
    public interface IServiceRepositoryAgentBuilder
    {
        /// <summary>
        /// Gets the service collection.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
