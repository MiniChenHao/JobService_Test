using System;
using Topshelf;

namespace JobService_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TopshelfExitCode rc = HostFactory.Run(X =>
            {
                X.Service<Job.TestJob>(Y =>
                {
                    Y.ConstructUsing(name => new Job.TestJob());
                    Y.WhenStarted(T => T.Start());
                    Y.WhenStopped(T => T.Stop());
                });

                X.RunAsLocalSystem();//服务使用NETWORK_SERVICE内置帐户运行。
                //身份标识，有好几种方式，如：X.RunAs("username", "password"); X.RunAsPrompt(); X.RunAsNetworkService(); 等

                X.SetDisplayName("自动服务");//显示名称
                X.SetDescription("任务调度系统的服务");//安装服务后，服务的描述
                X.SetServiceName("自动服务");//服务名称

                X.UseOwin(baseAddress: "http://localhost:9000/");

                X.SetStartTimeout(TimeSpan.FromMinutes(5));//设置超时之前等待服务启动的时间。 默认值为10秒。
                X.SetStopTimeout(TimeSpan.FromMinutes(35));//设置超时之前等待服务停止的时间。 默认值为10秒。

                //设置服务失败后的操作
                X.EnableServiceRecovery(r =>
                {
                    r.RestartService(1); //等待延迟时间（分钟）后重新启动服务
                });
            });
            Environment.ExitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Console.ReadLine();
        }
    }
}
