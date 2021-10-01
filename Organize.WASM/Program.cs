using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Organize.BusinessLogic;
using Organize.DataAccess;
using Organize.InMemoryStorage;
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
            builder.Services.AddScoped<IUserItemManager, UserItemManager>();

            builder.Services.AddScoped<IItemDataAccess, ItemDataAccess>();
            builder.Services.AddScoped<IPersistanceService, InMemoryStorage.InMemoryStorage>();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddScoped<ItemEditService>();

            var host = builder.Build();

            var persistanceService = host.Services.GetRequiredService<IPersistanceService>();
            await persistanceService.InitAsync();

            var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();
            var userItemManager = host.Services.GetRequiredService<IUserItemManager>();

            if (persistanceService is InMemoryStorage.InMemoryStorage)
            {
                TestData.CreateTestUser(userItemManager);
                currentUserService.CurrentUser = TestData.TestUser;
            }

            await host.RunAsync();
        }
    }
}
