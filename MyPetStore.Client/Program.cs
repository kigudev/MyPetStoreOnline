using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyPetStore.Client.Services.Implementations;
using MyPetStore.Client.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyPetStore.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("MyPetStore.Web", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(c => c.GetRequiredService<IHttpClientFactory>().CreateClient("MyPetStore.Web"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddScoped<IProductApi, ProductApi>();
            builder.Services.AddScoped<IBrandApi, BrandApi>();
            builder.Services.AddScoped<IOrderApi, OrderApi>();

            await builder.Build().RunAsync();
        }
    }
}