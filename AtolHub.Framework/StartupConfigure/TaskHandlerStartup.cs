using AtolHub.Core.Configuration;
using AtolHub.Core.Domain.Tasks;
using AtolHub.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace AtolHub.Framework.StartupConfigure
{
    /// <summary>
    /// Represents object for the configuring task on application startup
    /// </summary>
    public class TaskHandlerStartup : IStartup
    {
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        /// <summary>
        public void Configure(IApplicationBuilder application)
        {

        }

       
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
           //TODO:: Add logic to make Task Schedule Handler
           //TODO:: RUS Необходимо добавить логики для реализации и регистрации пакетных заданий
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order 
        {
            get { return 1010; }
        }
    }
}
