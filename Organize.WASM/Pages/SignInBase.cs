using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public class SignInBase : ComponentBase
    {
        protected string Day { get; } = DateTime.Now.DayOfWeek.ToString();

        protected string Username { get; set; } = "Test";
        protected User User { get; set; } = new();

        protected EditContext SignInEditContext { get; set; }

        // this method is auto-called once the component is created
        protected override void OnInitialized()
        {
            base.OnInitialized();
            SignInEditContext = new EditContext(User);
        }

        protected void HandleUsernameChanged(ChangeEventArgs eventArgs)
        {
            Username = eventArgs.Value.ToString();
        }

        protected void HandleUsernameValueChanged(string value)
        {
            Username = value;
        }

        public string GetError(Expression<Func<object>> expression)
        {
            if (SignInEditContext == null)
            {
                return null;
            }

            return SignInEditContext.GetValidationMessages(expression).FirstOrDefault();
        }
    }
}
