using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;
using AwesomeServerSample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Options;

namespace AwesomeServerSample
{
    public class AwesomeServer : IServer
    {
        public AwesomeServer(IServiceProvider serviceProvider, IOptions<AwesomeServerOptions> options)
        {
            var serverAddressesFeature = new ServerAddressesFeature();
            serverAddressesFeature.Addresses.Add(options.Value.FolderPath);

            Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            Features.Set<IHttpResponseFeature>(new HttpResponseFeature());
            Features.Set<IServerAddressesFeature>(serverAddressesFeature);
        }

        public IFeatureCollection Features { get; } = new FeatureCollection();

        public void Dispose() { }
        public void Start<TContext>(IHttpApplication<TContext> application)
        {        
            var watcher = new AwesomeFolderWatcher<TContext>(application, Features);

            watcher.Watch();  
        }
    }

}

namespace Microsoft.AspNetCore.Hosting
{
    public static class ServerExtensions
    {
        public static IWebHostBuilder UseAwesomeServer(this IWebHostBuilder hostBuilder, Action<AwesomeServerOptions> options)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.Configure(options);
                services.AddSingleton<IServer, AwesomeServer>();
            });
        }
    }
}
