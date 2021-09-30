﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Organize.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        private DotNetObjectReference<MainLayout> _dotnetReference;

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        public bool UseShortNavText { get; set; }

        protected void SignOut()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // blazorDimension is a global JavaScript object
            var jsWindowWidth = await JSRuntime.InvokeAsync<int>("blazorDimension.getWidth");
            CheckUseShortNavText(jsWindowWidth);

            _dotnetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("blazorResize.registerReferenceForResizeEvent", _dotnetReference);
        }

        [JSInvokable]
        public static void OnResize()
        {
            
        }

        [JSInvokable]
        public void HandleResize(int width, int height)
        {
            CheckUseShortNavText(width);

        }

        private void CheckUseShortNavText(int width)
        {
            var oldValue = UseShortNavText;
            UseShortNavText = width < 700;

            if (oldValue != UseShortNavText)
            {
                StateHasChanged();
            }
        }

        public void Dispose()
        {
            _dotnetReference?.Dispose();
        }
    }
}
