using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechNationFinanceiroClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddHttpClient<INotaFiscalService, NotaFiscalService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:32800/api/");
});
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
