using System;

namespace ServiceGovernance.Repository.Agent.Configuration
{
    /// <summary>
    /// Options for the repository agent
    /// </summary>
    public class RepositoryAgentOptions
    {
        /// <summary>
        /// Gets or sets the uri of the repository
        /// </summary>
        public Uri Repository { get; set; }

        /// <summary>
        /// Gets or sets a unique service identifier
        /// </summary>
        public string ServiceIdentifier { get; set; }

        /// <summary>
        /// Validate the option's values
        /// </summary>
        public void Validate()
        {
            if (Repository == null)
                throw new ConfigurationException("The repository uri is not defined!", nameof(Repository));

            if (string.IsNullOrWhiteSpace(ServiceIdentifier))
                throw new ConfigurationException("ServiceIdentifier is not defined!", nameof(ServiceIdentifier));
        }
    }
}
