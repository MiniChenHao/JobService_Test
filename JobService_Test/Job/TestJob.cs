using System;
using System.Timers;

namespace JobService_Test.Job
{
    /// <summary>
    /// 
    /// </summary>
    public class TestJob
    {
        readonly Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        public TestJob()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start() { _timer.Start(); }

        /// <summary>
        /// 
        /// </summary>
        public void Stop() { _timer.Stop(); }
    }
}
