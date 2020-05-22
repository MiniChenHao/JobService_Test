using Autofac;
using Autofac.Core;
using Hangfire.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobService_Test
{
    /// <summary>
    /// Hangfire Module
    /// </summary>
    public class HangfireModule : Autofac.Module
    {
        /// <summary>
        /// 附加到组件注册
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);

            // 处理构造函数参数。
            registration.Preparing += OnComponentPreparing;

            // 处理属性。
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }

        /// <summary>
        /// 注入记录器属性
        /// </summary>
        /// <param name="instance"></param>
        private void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // 获取所有可设置的属性。
            // 如果您想确保这些属性只是UNSET属性，
            // 这是您要做的地方。
            var properties = instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // 设置位于的属性。
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, LogProvider.GetLogger(instanceType), null);
            }
        }

        /// <summary>
        /// 关于组件准备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(new[] { new ResolvedParameter((p, i) => p.ParameterType == typeof(ILog), (p, i) => LogProvider.GetLogger(p.Member.DeclaringType)), });
        }

        /// <summary>
        /// Auto register
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //注册所有已实现的接口
            builder.RegisterAssemblyTypes(ThisAssembly).Where(t => typeof(Jobs.IDependency).IsAssignableFrom(t) && t != typeof(Jobs.IDependency) && !t.IsInterface).AsImplementedInterfaces();

            //在此处注册指定的类型
            builder.Register(x => new Jobs.RecurringJobService());
        }
    }
}
