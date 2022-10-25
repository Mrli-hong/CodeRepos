using DependncyInjectionDemo1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


//ServiceCollection services = new ServiceCollection();
////注册服务
//services.AddTransient<TestServiceImpl>();
//services.AddSingleton<TestServiceImp2>();
////创建服务定位器
//using (ServiceProvider serviceProvider = services.BuildServiceProvider())
//{
//    TestServiceImpl t = serviceProvider.GetService<TestServiceImpl>();
//    TestServiceImp2 tt = serviceProvider.GetService<TestServiceImp2>();
//    tt.Say();
//    t.Name = "小明";
//    t.SayHi();
//    using (IServiceScope scope = serviceProvider.CreateScope())
//    {
//        TestServiceImpl t1 = scope.ServiceProvider.GetService<TestServiceImpl>();
//        TestServiceImpl t2 = scope.ServiceProvider.GetService<TestServiceImpl>();
//        Console.WriteLine(object.ReferenceEquals(t, t2));
//        Console.WriteLine(object.ReferenceEquals(t1, t2));
//    }
//}

//例子2：
ConfigCommand.Test(args);