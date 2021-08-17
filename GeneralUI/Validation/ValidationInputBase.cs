﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralUI.Validation
{
    public class ValidationInputBase : ComponentBase
    {
        // ChildPropertyName and ChildPropertyNameChanged has to exist for @bind-ChildPropertyName="@ParentPropertyName" to work inside the parent component
        [Parameter]
        public string Value { get; set; }
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Error { get; set; }
        [Parameter(CaptureUnmatchedValues =true)]
        public Dictionary<string,object> InputAttributes { get; set; }
        
        protected async void HandleInputChanged(ChangeEventArgs eventArgs)
        {
            await ValueChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}