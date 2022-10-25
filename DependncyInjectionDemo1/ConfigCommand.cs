using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependncyInjectionDemo1
{
     public class TestOptions
    {
        private readonly IOptionsSnapshot<Setting> config;
        public TestOptions(IOptionsSnapshot<Setting> config)
        {
            this.config = config;
        }

        public void Test()
        {
            Console.WriteLine(config.Value.Name);
            Console.WriteLine(config.Value.Proxy.Address);
            Console.WriteLine(config.Value.Proxy.Port);
            Console.WriteLine(config.Value.Ids[0]);
        }
    }
    internal class ConfigCommand
    {
        public static void Test(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddCommandLine(args);
            service.AddScoped<TestOptions>();
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            service.AddOptions().Configure<Setting>(e => configurationRoot.Bind(e));
            using (var sp = service.BuildServiceProvider())
            {
                var result = sp.GetService<TestOptions>();
                result.Test();
            }
        }
    }
}
