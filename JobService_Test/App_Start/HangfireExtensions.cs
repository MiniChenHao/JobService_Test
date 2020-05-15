using Hangfire;
using Owin;
using System;
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
    }
}
