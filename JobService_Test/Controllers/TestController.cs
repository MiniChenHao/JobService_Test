using Hangfire;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobService_Test.Controllers
{
    /// <summary>
    /// 测试 控制器
    /// </summary>
    public class TestController : ApiController
    {
        /// <summary>
        /// 每分钟执行控制台作业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SimpleJob()
        {
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("{0} Recurring job completed successfully!", DateTime.Now.ToString()), Cron.Minutely);
            //BackgroundJob.Enqueue<AppService.ISampleService>(x => x.SimpleJob(null));
            RecurringJob.AddOrUpdate<ISampleService>("TestSimpleJob", X => X.SimpleJob(null), Cron.Minutely(), TimeZoneInfo.Local);
            return Request.CreateResponse(HttpStatusCode.OK, "Create TestSimpleJob  Successfully");
        }
    }
}
