using FluentValidation.AspNetCore;
using AtolHub.Framework.Mvc.Routing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using AtolHub.Core.Configuration;
using AtolHub.Core.Infrastructure;

namespace AtolHub.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        /// <returns>Configured service provider</returns>
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            //add GrandConfig configuration parameters            
            services.ConfigureStartupConfig<BaseConfig>(configuration.GetSection("Base"));
            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services, configuration);
            engine.ConfigureServices(services, configuration);
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();
            //bind it to the appropriate section of configuration
            configuration.Bind(config);
            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddGrandMvc(this IServiceCollection services, IConfiguration configuration)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            var config = new BaseConfig();
            configuration.GetSection("Base").Bind(config);

            //set compatibility version
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //add fluent validation
            mvcBuilder.AddFluentValidation(configuration =>
            {
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("AtolHub", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);

                configuration.RegisterValidatorsFromAssemblies(assemblies);
                configuration.RunDefaultMvcValidationAfterFluentValidationExecutes = false;

                configuration.ImplicitlyValidateChildProperties = true;
            });

            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }

        /// <summary>
        /// Adds services for WebEncoderOptions
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddWebEncoder(this IServiceCollection services)
        {          
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
        }

        /// <summary>
        /// Adds services for mediatR
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddMediator(this IServiceCollection services)
        {
            var typeFinder = new WebAppTypeFinder();
            var assemblies = typeFinder.GetAssemblies();
            services.AddMediatR(assemblies.ToArray());
        }
    }
}