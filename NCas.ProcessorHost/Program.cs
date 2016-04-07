using Topshelf;

namespace NCas.ProcessorHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (!Environment.UserInteractive)
            //{
            //    ServiceBase.Run(new Service1());
            //}
            //else
            //{
            //    Bootstrap.Initialize();
            //    Bootstrap.Start();
            //    Console.WriteLine("Press Enter to exit...");
            //    Console.ReadLine();
            //}


            HostFactory.Run(x =>
            {
                x.Service<Bootstrap>(s =>
                {
                    s.ConstructUsing(name => new Bootstrap());
                    s.WhenStarted(bs =>
                    {
                        bs.Initialize();
                        bs.Start();
                    });
                    s.WhenStopped(bs => bs.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("NCas Processor Host");
                x.SetDisplayName("NCasENodeHost");
                x.SetServiceName("NCasENodeHost");
            }); 

        }
    }
}
