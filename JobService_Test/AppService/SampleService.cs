using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService_Test.AppService
{
    /// <summary>
    /// 
    /// </summary>
    public class SampleService : BaseAppService, ISampleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void SimpleJob(PerformContext context)
        {
            context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} SimpleJob Running ...");

            var progressBar = context.WriteProgressBar();

            foreach (var i in Enumerable.Range(1, 50).ToList().WithProgress(progressBar))
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
