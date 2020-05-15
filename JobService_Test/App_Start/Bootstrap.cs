using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Logging;

namespace JobService_Test
{
    /// <summary>
    /// OWIN host
    /// </summary>
    public class Bootstrap : ServiceControl
    {
        private readonly LogWriter _logger = HostLogger.Get(typeof(Bootstrap));
        private IDisposable webApp;

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            try
            {
                webApp = WebApp.Start<Startup>(Address);
                Process.Start($@"{Address}/swagger/ui/index");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Topshelf 启动发生的错误:{0}", ex.ToString()));
                return false;
            }

        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Stop(HostControl hostControl)
        {
            try
            {
                webApp?.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Topshelf 停止发生的错误:{0}", ex.ToString()));
                return false;
            }

        }
    }
}
