using AtolHub.Core.Configuration;
using AtolHub.Core.Infrastructure;
using AtolHub.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AtolHub.Framework.StartupConfigure
{
    /// <summary>
    /// Represents object for the configuring MVC on application startup
    /// </summary>
    public class BaseMvcStartup : IStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add mediatR
            services.AddMediator();

            //add and configure MVC feature
            services.AddGrandMvc(configuration);

            var config = new BaseConfig();
            configuration.GetSection("Base").Bind(config);
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {            
            application.UseRouting();

            //MVC routing
            application.UseBaseMvc();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            //MVC should be loaded last
            get { return 1000; }
        }
    }
}
