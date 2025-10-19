using DataGeneratorBlazor.Client.Pages;
using DataGeneratorBlazor.Components;

namespace DataGeneratorBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient("generator", httpClient =>
            {
                var url = builder.Configuration["Endpoint"] ?? throw new Exception("API endpoint not configured in appsettings");
                httpClient.BaseAddress = new Uri(url);

            });

            var app = builder.Build();
            app.MapDefaultEndpoints();

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
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(Counter).Assembly);

            app.Run();
        }
    }
}
