using Hangfire;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService_Test.AppService
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
        [RecurringJob("* * * * *")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("SimpleJobTest")]
        [Queue("jobs")]
        void SimpleJob(PerformContext context);
    }
}
