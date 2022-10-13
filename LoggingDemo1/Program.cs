using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
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

                //采用NLog进行输出，相关输出的内容在nlog.config文件中进行配置
                //loggerBuilder.AddNLog();
                
                //采用Serilog进行格式化输出
                Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Debug()
                             .Enrich.FromLogContext()
                             .WriteTo.Console(new JsonFormatter())
                             .CreateLogger();
                loggerBuilder.AddSerilog();

                //设置最低显示级别
                //loggerBuilder.SetMinimumLevel(LogLevel.Trace);
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
