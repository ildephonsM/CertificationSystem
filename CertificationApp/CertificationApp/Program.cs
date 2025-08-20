using CertificationApp.Components;
using CertificationApp.Data;
using Microsoft.EntityFrameworkCore;


namespace CertificationApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            // ---------- REGISTER DbContext ----------
            builder.Services.AddDbContext<CertificationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ---------- ADD CONTROLLERS for API endpoints ----------
            builder.Services.AddControllers();  // needed if you use API controllers
            builder.Services.AddEndpointsApiExplorer();

            // ---------- REGISTER HttpClient for server-side rendering ----------
            builder.Services.AddHttpClient("ServerAPI", client =>
            {
                // Ensure this matches your server's base URL
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7222/");
            });

            // Default HttpClient DI binding
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAntiforgery();

            // Map API controllers
            app.MapControllers();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
