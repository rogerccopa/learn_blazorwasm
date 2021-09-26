using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Organize.BusinessLogic;
using Organize.Shared.Contracts;
using Organize.TestFake;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Organize.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //builder.Services.AddSingleton<IUserManager, UserManager>();
            builder.Services.AddScoped<IUserManager, UserManagerFake>();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddScoped<ItemEditService>();

            var host = builder.Build();
            var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();
            TestData.CreateTestUser();
            currentUserService.CurrentUser = TestData.TestUser;

            await host.RunAsync();
        }
    }
}
