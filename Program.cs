using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechNationFinanceiroClient;
using TechNationFinanceiroClient.Services;
using TechNationFinanceiroClient.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configura o componente raiz da aplicação
builder.RootComponents.Add<App>("#app");


builder.Services.AddTransient<AuthMessageHandler>();

// Configuração do serviço HTTP para INotaFiscalService
builder.Services.AddHttpClient<INotaFiscalService, NotaFiscalService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/");
}).AddHttpMessageHandler<AuthMessageHandler>();

// Configuração do serviço HTTP para IAuthenticationService
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/"); 
});

// Adiciona o AuthorizationCore para registrar IAuthorizationPolicyProvider
builder.Services.AddAuthorizationCore();


//authenticate
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Configuração opcional para manipular o <head> do documento
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuração para injeção de HttpClient configurado com a base address do host
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// Constrói e executa a aplicação
await builder.Build().RunAsync();
