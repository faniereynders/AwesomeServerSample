using Microsoft.AspNetCore.Hosting;

namespace AwesomeServerSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseAwesomeServer(o=>o.FolderPath = @"C:\process")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

}
