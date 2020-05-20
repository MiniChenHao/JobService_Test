using Hangfire.Logging;
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
    public class BaseAppService
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual ILog Logger { get; set; } = new NullLogger();
    }
}
