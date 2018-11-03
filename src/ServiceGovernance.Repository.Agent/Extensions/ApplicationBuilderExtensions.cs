using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            app.Validate();

            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            var client = app.ApplicationServices.GetRequiredService<IServiceRepositoryClient>();

            // publish service's api when it's running
            lifetime.ApplicationStarted.Register(() => client.PublishServiceApi());

            return app;
        }

        /// <summary>
        /// Validates the registration and configuration of the agent
        /// </summary>
        /// <param name="app"></param>
        internal static void Validate(this IApplicationBuilder app)
        {
            if (!(app.ApplicationServices.GetService(typeof(ILoggerFactory)) is ILoggerFactory loggerFactory))
                throw new InvalidOperationException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("ServiceRegistry.Agent.Startup");

            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                serviceProvider.TestService(typeof(IApiDescriptionProvider), logger, "No Api description provider specified. Provide an implementation for 'IApiDescriptionProvider' (e.g. ServiceGovernance.Repository.Agent.SwaggerV3).");
            }
        }

        internal static object TestService(this IServiceProvider serviceProvider, Type service, ILogger logger, string message)
        {
            var appService = serviceProvider.GetService(service);

            if (appService == null)
            {
                logger.LogCritical(message);

                throw new InvalidOperationException(message);
            }

            return appService;
        }
    }
}
