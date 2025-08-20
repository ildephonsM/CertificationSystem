using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


namespace CertificationApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Register HttpClient for dependency injection
            // Point to your Server API
            builder.Services.AddScoped(sp => new HttpClient
            {
                //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                BaseAddress = new Uri("https://localhost:7222/") // your API base URL
            });

            await builder.Build().RunAsync();
        }
    }
}
