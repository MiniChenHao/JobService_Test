using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Integration.WebApi;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.RecurringJobExtensions;
using Hangfire.SqlServer;
using Owin;
using System;
using System.Linq;
using Topshelf;
using Topshelf.HostConfigurators;

namespace JobService_Test
{
    /// <summary>
    /// Hangfire扩展
    /// </summary>
    public static class HangfireExtensions
    {
        /// <summary>
        /// 使用OWIN
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public static HostConfigurator UseOwin(this HostConfigurator configurator, string baseAddress)
        {
            if (string.IsNullOrEmpty(baseAddress)) throw new ArgumentNullException(nameof(baseAddress));
            { configurator.Service(() => new Bootstrap { Address = baseAddress }); }
            return configurator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStorage"></typeparam>
        /// <param name="app"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        public static IGlobalConfiguration<TStorage> UseStorage<TStorage>(this IAppBuilder app, TStorage storage) where TStorage : JobStorage
        {
            if (storage == null) { throw new ArgumentNullException(nameof(storage)); }
            return GlobalConfiguration.Configuration.UseStorage(storage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder UseDashboardMetric(this IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.ServerCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(SqlServerStorage.ActiveConnections);
            GlobalConfiguration.Configuration.UseDashboardMetric(SqlServerStorage.TotalConnections);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.RecurringJobCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.RetriesCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.AwaitingCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.ScheduledCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.ProcessingCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.SucceededCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.FailedCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.DeletedCount);
            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IContainer UseAutofac(this IAppBuilder app, System.Web.Http.HttpConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var builder = new ContainerBuilder();
            var assembly = typeof(Startup).Assembly;
            builder.RegisterAssemblyModules(assembly);
            builder.RegisterApiControllers(assembly);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            return container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IAppBuilder UseRecurringJob(this IAppBuilder app, params Type[] types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            GlobalConfiguration.Configuration.UseRecurringJob(types);
            return app;
        }
    }
}
