using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("generator", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:5123/");

});

await builder.Build().RunAsync();
