using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Recipes.Shared.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recipes.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("Recipes.ServerAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // another client for managin anonymous calls from client

            builder.Services.AddHttpClient("Recipes.Anonymous",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));



            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("Recipes.ServerAPI"));



            builder.Services.AddApiAuthorization();
            builder.Services.AddAuthorizationCore(o =>
            {
                o.AddPolicy("EditDeletePolicy", policy =>
                    policy.Requirements.Add(new OperationAuthorizationRequirement()));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, RecipeAuthorizationHandler>();

            builder.Services
            .AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
