using Hangfire.Logging;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseAppService
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual ILog Logger { get; set; } = new NullLogger();
    }
}
