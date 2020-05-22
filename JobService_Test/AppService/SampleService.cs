using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Linq;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public class SampleService : BaseAppService, ISampleService
    {
        private ISampleService _sampleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleService"></param>
        public SampleService(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

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
