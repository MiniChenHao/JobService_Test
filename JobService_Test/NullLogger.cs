using Hangfire.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public class NullLogger : ILog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="messageFunc"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="messageFunc"></param>
        /// <param name="exception"></param>
        /// <param name="formatParameters"></param>
        /// <returns></returns>
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            return true;
        }
    }
}
