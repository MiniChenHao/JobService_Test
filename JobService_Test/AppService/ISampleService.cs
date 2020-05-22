using Hangfire;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System.ComponentModel;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISampleService : IAppService
    {
        /// <summary>
        /// simple job test
        /// </summary>
        /// <param name="context"></param>
        [DisplayName("Creating order from product, productId:{0}")]
        [Queue("jobs")]
        void SimpleJob(PerformContext context);
    }
}
