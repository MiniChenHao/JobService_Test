using Hangfire.Logging;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppService : Jobs.IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        ILog Logger { get; set; }
    }
}
