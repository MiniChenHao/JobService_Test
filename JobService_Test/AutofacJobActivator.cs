using Autofac;
using Hangfire;
using Hangfire.Annotations;
using System;

namespace JobService_Test
{
    /// <summary>
    /// Hangfire Job Activator based on Autofac IoC Container.
    /// </summary>
    public class AutofacJobActivator : JobActivator
    {
        /// <summary>
        /// Tag used in setting up per-job lifetime scope registrations.
        /// </summary>
        public static readonly object LifetimeScopeTag = "BackgroundJobScope";

        private readonly ILifetimeScope _lifetimeScope;
        private readonly bool _useTaggedLifetimeScope;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lifetimeScope"></param>
        /// <param name="useTaggedLifetimeScope"></param>
        public AutofacJobActivator([NotNull] ILifetimeScope lifetimeScope, bool useTaggedLifetimeScope = true)
        {
            if (lifetimeScope == null) throw new ArgumentNullException("lifetimeScope");
            _lifetimeScope = lifetimeScope;
            _useTaggedLifetimeScope = useTaggedLifetimeScope;
        }

        /// <inheritdoc />
        public override object ActivateJob(Type jobType)
        {
            return _lifetimeScope.Resolve(jobType);
        }

#if NET45
        /// <summary>
        /// 开始范围
        /// </summary>
        /// <returns></returns>
        public override JobActivatorScope BeginScope()
        {
            ILifetimeScope lifetimeScope = _useTaggedLifetimeScope ? _lifetimeScope.BeginLifetimeScope(LifetimeScopeTag) : _lifetimeScope.BeginLifetimeScope();
            return new AutofacScope(lifetimeScope);
        }
#else
        /// <summary>
        /// 开始范围
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            ILifetimeScope lifetimeScope = _useTaggedLifetimeScope ? _lifetimeScope.BeginLifetimeScope(LifetimeScopeTag) : _lifetimeScope.BeginLifetimeScope();
            return new AutofacScope(lifetimeScope);
        }
#endif

        class AutofacScope : JobActivatorScope
        {
            private readonly ILifetimeScope _lifetimeScope;

            public AutofacScope(ILifetimeScope lifetimeScope)
            {
                _lifetimeScope = lifetimeScope;
            }

            public override object Resolve(Type type)
            {
                return _lifetimeScope.Resolve(type);
            }

            public override void DisposeScope()
            {
                _lifetimeScope.Dispose();
            }
        }
    }
}
