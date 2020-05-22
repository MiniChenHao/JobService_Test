using Autofac;
using Hangfire;
using System;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public class ContainerJobActivator : JobActivator
    {
        private IContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ContainerJobActivator(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override object ActivateJob(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
