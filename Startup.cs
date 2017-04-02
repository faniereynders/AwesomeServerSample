using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AwesomeServerSample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            var logger = loggerFactory.CreateLogger<Program>();

            app.MapWhen(c => c.Request.Path == "/test1", config =>
               {
                   config.Run(async (context) =>
                   {
                       logger.LogInformation("Invoking Test1 endpoint");
                       context.Response.StatusCode = 200;
                       await context.Response.WriteAsync("Hello from Test1!");
                   });
               })
             .MapWhen(c => c.Request.Path == "/test2", config =>
             {
                 config.Run(async (context) =>
                 {
                     context.Response.StatusCode = 200;
                     logger.LogInformation("Invoking Test2 endpoint");
                     await context.Response.WriteAsync("HELLO FROM TEST2!");
                 });
             })
             .Run(async (context) =>
             {
                 context.Response.StatusCode = 404;
                 logger.LogInformation("Invoking NotFound endpoint");
                 await context.Response.WriteAsync("Not Found");
             });            
        }
    }
}
