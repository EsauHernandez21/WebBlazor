using BlazorAppWeb;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Configura HttpClient para que apunte a la URL base de tu API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5150/") });


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

// Clase de configuración que representa la estructura del JSON
public class JsonConfiguration
{
    public ApiSettings ApiSettings { get; set; }
}

public class ApiSettings
{
    public string BaseUrl { get; set; }
}
