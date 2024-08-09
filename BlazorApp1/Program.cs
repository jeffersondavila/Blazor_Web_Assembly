using BlazorApp1;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Obtener la URL desde appsettings.json
var apiUrl = builder.Configuration.GetValue<string>("apiPlatzi");

// Configurar HttpClient con la URL base
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

// Registrar servicios que dependen de HttpClient
builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<ICategoryService, CategoryServices>();

await builder.Build().RunAsync();
