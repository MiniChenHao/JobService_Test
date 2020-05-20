using Microsoft.Owin;
using Owin;
using Hangfire.SqlServer;
using System.Web.Http;
using Swashbuckle.Application;
using Hangfire.Console;
using Hangfire;
using System.Net.Http.Formatting;
using Autofac;
using System;

[assembly: OwinStartup(typeof(JobService_Test.Startup))]

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            HttpConfiguration config = new HttpConfiguration();

            //配置路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //配置Swagger
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "JobServiceAPI");
                c.IncludeXmlComments($@"{typeof(Startup).Assembly.GetName().Name}.xml");
            }).EnableSwaggerUi();

            //将默认XML返回数据格式改为JSON
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("datatype", "json", "application/json"));

            app.UseWebApi(config);

            //app.UseHangfireServer(additionalProcesses: new[] { new ProcessMonitor(checkInterval: TimeSpan.FromSeconds(1)) });

            IContainer container = app.UseAutofac(config);
            var queues = new[] { "default", "apis", "jobs" };
            app.UseStorage(new SqlServerStorage("Data Source=localhost;Initial Catalog=ShenOnlineJob;Persist Security Info=True;User ID=sa;Password=123")).UseConsole();
            app.UseHangfireServer(new BackgroundJobServerOptions { ShutdownTimeout = TimeSpan.FromMinutes(30), Queues = queues, WorkerCount = Math.Max(Environment.ProcessorCount, 20) });
            app.UseHangfireDashboard();
            app.UseDashboardMetric();
            app.UseHangfireServer();

            //app.UseRecurringJob(typeof(Jobs.RecurringJobService));
            app.UseRecurringJob(container);
        }
    }
}
