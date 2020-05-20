using Hangfire;
using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System;
using System.ComponentModel;
using System.Linq;

namespace JobService_Test.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class RecurringJobService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [RecurringJob("*/1 * * * *")]
        [DisplayName("InstanceTestJob")]
        [Queue("jobs")]
        public void InstanceTestJob(PerformContext context)
        {
            context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} InstanceTestJob Running ...");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [RecurringJob("* * * * *")]
        [DisplayName("SimpleJob")]
        [Queue("default")]
        public void SimpleJob(PerformContext context)
        {
            context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} SimpleJob Running ...");
            var progressBar = context.WriteProgressBar();
            foreach (var i in Enumerable.Range(1, 50).ToList().WithProgress(progressBar))
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [RecurringJob("*/5 * * * *")]
        [DisplayName("JobStaticTest")]
        [Queue("jobs")]
        public static void StaticTestJob(PerformContext context)
        {
            context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} StaticTestJob Running ...");
        }
    }
}
