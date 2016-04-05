using System;
using GUtils.Autofac;
using GUtils.JsonNet;
using GUtils.Log4Net;
using NCas.Core.TicketGrantings;

namespace NCas.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GUtils.Configurations.Configuration
                .Create()
                .UseAutofac() //设置依赖注入工具,必须
                .RegisterCommonComponents() //注册所有组件,必须
                .UseJsonNet()
                .UseLog4Net();
            //var appAccount = new AppAccount(Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"),"测试杀杀杀11",
            //    "http://www.baidu.com", "http://www.baidu.com/verify?login=3", "http://www.baidu.com/notify?u11=333http://www.baidu.com/notify?u11=333");
            var c = new TicketGranting(new AccountInfo("", "12323232", "10023232303", "张23123123123三"));

            var r = c.EncodeCookie();
            Console.WriteLine(r);
            Console.ReadKey();

        }
    }
}
