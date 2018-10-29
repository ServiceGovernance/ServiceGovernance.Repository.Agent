using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServiceGovernance.Repository.Agent;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Pipeline extension methods for adding repository agent
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds repository agent to the pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseServiceRepositoryAgent(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            var client = app.ApplicationServices.GetRequiredService<IServiceRepositoryClient>();

            //// register service when it's running
            //lifetime.ApplicationStarted.Register(() => client.RegisterService());

            //// remove registration when service is shutting down
            //lifetime.ApplicationStopping.Register(() => client.UnregisterService());

            return app;
        }
    }
}
