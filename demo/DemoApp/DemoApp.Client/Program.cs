using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazor.Analytics;
using System.Threading.Tasks;

namespace DemoApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddGoogleAnalytics("UA-111742878-2");

            await builder.Build().RunAsync();
        }
    }
}
