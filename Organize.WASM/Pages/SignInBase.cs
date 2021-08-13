using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public class SignInBase : ComponentBase
    {
        protected string Day { get; } = DateTime.Now.DayOfWeek.ToString();

        protected string Username { get; set; } = "Test";
        protected User User { get; set; } = new();

        protected void HandleUsernameChanged(ChangeEventArgs eventArgs)
        {
            Username = eventArgs.Value.ToString();
        }

        protected void HandleUsernameValueChanged(string value)
        {
            Username = value;
        }
    }
}
