using AtolHub.Core.Configuration;
using AtolHub.Core.Infrastructure;
using AtolHub.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace FieldService.Framework.StartupConfigure
{
    /// <summary>
    /// Represents object for the configuring common features and middleware on application startup
    /// </summary>
    public class CommonStartup : IStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new BaseConfig();
            configuration.GetSection("Base").Bind(config);

            //compression
            services.AddResponseCompression();

            //add options feature
            services.AddOptions();

            //add localization
            services.AddLocalization();

            //add WebEncoderOptions
            services.AddWebEncoder();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            var serviceProvider = application.ApplicationServices;
            var grandConfig = serviceProvider.GetRequiredService<BaseConfig>();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            //common services should be loaded after error handlers
            get { return 100; }
        }
    }
}
