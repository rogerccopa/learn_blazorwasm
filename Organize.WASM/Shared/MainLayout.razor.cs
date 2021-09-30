using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Organize.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        protected void SignOut()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // blazorDimension is a global JavaScript object
            var jsWindowWidth = await JSRuntime.InvokeAsync<int>("blazorDimension.getWidth");

            Console.WriteLine($"width received from js: {jsWindowWidth}");

            var reference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("blazorResize.registerReferenceForResizeEvent", reference);
        }

        [JSInvokable]
        public static void OnResize()
        {
            Console.WriteLine("OnResize from C#.Net");
        }

        [JSInvokable]
        public void HandleResize(int width, int height)
        {
            Console.WriteLine($"C# received width: {width}");
            Console.WriteLine($"C# received height: {height}");
        }
    }
}
