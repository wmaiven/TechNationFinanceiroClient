using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;
using TechNationFinanceiroClient;
using TechNationFinanceiroClient.Services;
using TechNationFinanceiroClient.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configura o componente raiz da aplicação
builder.RootComponents.Add<App>("#app");

// Configuração do serviço HTTP para INotaFiscalService
builder.Services.AddHttpClient<INotaFiscalService, NotaFiscalService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/"); // URL da API
});

// Configuração opcional para manipular o <head> do documento
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuração para injeção de HttpClient configurado com a base address do host
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// Registrar AuthenticationService com a interface IAuthenticationService
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Constrói e executa a aplicação
await builder.Build().RunAsync();
