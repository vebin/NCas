using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUtils.Autofac;
using GUtils.JsonNet;
using NCas.Core.TicketGrantingCookies;
using GUtils.Log4Net;

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

            TicketGrantingCookie c = new TicketGrantingCookie(  new AccountInfo("12323232", "10023232303", "张23123123123三"));

            var r = c.EncodeCookie();
            Console.WriteLine(r);
            Console.ReadKey();

        }
    }
}
