using Microsoft.OpenApi.Models;
using System;

namespace ServiceGovernance.Repository.Agent
{
    /// <summary>
    /// Abstraction for provider describing Apis
    /// </summary>
    public interface IApiDescriptionProvider
    {
        /// <summary>
        /// Get the api descriptions
        /// </summary>
        /// <returns></returns>
        OpenApiDocument GetDescriptions();
    }
}
