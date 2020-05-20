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
    public interface IAppService : Jobs.IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        ILog Logger { get; set; }
    }
}
