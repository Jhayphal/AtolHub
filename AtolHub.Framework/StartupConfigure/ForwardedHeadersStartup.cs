using AtolHub.Core.Configuration;
using AtolHub.Core.Infrastructure;
using AtolHub.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtolHub.Framework.StartupConfigure
{
    public class ForwardedHeadersStartup : IStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            var serviceProvider = application.ApplicationServices;
            var hostingConfig = serviceProvider.GetRequiredService<HostingConfig>();

            if (hostingConfig.UseForwardedHeaders)
                application.UseGrandForwardedHeaders();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            //ForwardedHeadersStartup should be loaded before authentication 
            get { return 0; }
        }
    }
}
