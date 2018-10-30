using System.Threading.Tasks;

namespace ServiceGovernance.Repository.Agent
{
    /// <summary>
    /// Interface to abstract service repository client functions
    /// </summary>
    public interface IServiceRepositoryClient
    {
        /// <summary>
        /// Publishes the API to the service repository
        /// </summary>
        /// <returns></returns>
        Task PublishServiceApiAsync();
    }
}
