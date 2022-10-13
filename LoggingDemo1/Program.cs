using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace LoggingDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
      
            //注册日志服务，并增加配置AddConsole
            services.AddLogging(loggerBuilder=> {
                //loggerBuilder.AddConsole();
                //增加包Microsoft.Extensions.Logging.EventLog
                //loggerBuilder.AddEventLog();

                loggerBuilder.AddNLog();

                //设置最低显示级别
                loggerBuilder.SetMinimumLevel(LogLevel.Trace);
            });

            services.AddScoped<NLogTest>();

            using (var sp = services.BuildServiceProvider())
            {
                var log = sp.GetService<NLogTest>();
                log.Test();
            }
        }
    }
}
