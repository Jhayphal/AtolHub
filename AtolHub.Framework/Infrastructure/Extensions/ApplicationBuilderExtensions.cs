using AtolHub.Core;
using AtolHub.Core.Configuration;
using AtolHub.Core.Infrastructure;
using Autofac;
using AtolHub.Framework.Mvc.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AtolHub.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        /// <summary>
        /// Configure container
        /// </summary>
        /// <param name="container">ContainerBuilder from autofac</param>
        /// <param name="configuration">configuration</param>
        public static void ConfigureContainer(this ContainerBuilder container, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            EngineContext.Current.ConfigureContainer(container, configuration);
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseGrandExceptionHandler(this IApplicationBuilder application)
        {
            var serviceProvider = application.ApplicationServices;
            var сonfig = serviceProvider.GetRequiredService<BaseConfig>();
            var hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            bool useDetailedExceptionPage =  hostingEnvironment.IsDevelopment();
            if (useDetailedExceptionPage)
            {
                //get detailed exceptions for developing and testing purposes
                application.UseDeveloperExceptionPage();
            }         
        }

        /// <summary>
        /// Adds a special handler that checks for responses with the 400 status code (bad request)
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseBadRequestResult(this IApplicationBuilder application)
        {
            var serviceProvider = application.ApplicationServices;
            application.UseStatusCodePages(context =>
            {
                //handle 404 (Bad request)
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                {

                }

                return Task.CompletedTask;
            });

        }

         /// <summary>
        /// Configure MVC routing
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseBaseMvc(this IApplicationBuilder application)
        {
            application.UseMvc(routeBuilder =>
            {
                //register all routes
                routeBuilder.ServiceProvider.GetRequiredService<IRoutePublisher>().RegisterRoutes(routeBuilder);
            });
        }
       
        /// <summary>
        /// Configure UseForwardedHeaders
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseGrandForwardedHeaders(this IApplicationBuilder application)
        {
            application.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
