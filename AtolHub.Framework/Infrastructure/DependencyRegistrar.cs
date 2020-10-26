using Autofac;
using FluentValidation;
using AtolHub.Core;
using AtolHub.Core.Configuration;
using AtolHub.Core.Data;
using AtolHub.Core.Infrastructure;
using AtolHub.Core.Infrastructure.Data;
using AtolHub.Core.Infrastructure.DependencyManagement;
using AtolHub.Framework.Mvc.Routing;
using Microsoft.AspNetCore.StaticFiles;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Reflection;

namespace AtolHub.Framework.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, BaseConfig config)
        {
            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var connectionString = config.DatabaseConnectionString;
            var dataProviderSettings = dataSettingsManager.LoadSettings(connectionString);
            builder.Register(c => dataSettingsManager.LoadSettings(config.DatabaseConnectionString)).As<DataSettings>();
            builder.Register(x => new MongoDBDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            var databaseName = new MongoUrl(connectionString).DatabaseName;
            builder.Register(c => new MongoClient(connectionString).GetDatabase(databaseName)).SingleInstance();
            builder.Register<IMongoDBContext>(c => new MongoDBContext(connectionString)).InstancePerLifetimeScope();

            ////MongoDbRepository
            builder.RegisterGeneric(typeof(MongoDBRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            builder.RegisterType<BaseConfig>().SingleInstance();
            
            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            var provider = new FileExtensionContentTypeProvider();   
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();

            var validators = typeFinder.FindClassesOfType(typeof(IValidator)).ToList();
            foreach (var validator in validators)
            {
                builder.RegisterType(validator);
            }
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order 
        {
            get { return 0; }
        }
    }
}
