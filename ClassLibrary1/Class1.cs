using Microsoft.Extensions.DependencyInjection;
using Zack.Commons;

namespace ClassLibrary1
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<Class1>();
        }
    }

    public class Class1 
    {
        public string ConsoleService()
        {
            return "CONSOLE SERVICE IN CLASS1";
        }
    }
}