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
    public class SignBase : ComponentBase
    {
        protected User User { get; set; } = new();

        protected EditContext MyEditContext { get; set; }

        // this method is auto-called once the component is created
        protected override void OnInitialized()
        {
            base.OnInitialized();
            MyEditContext = new EditContext(User);
        }

        public string GetError(Expression<Func<object>> expression)
        {
            if (MyEditContext == null)
            {
                return null;
            }

            return MyEditContext.GetValidationMessages(expression).FirstOrDefault();
        }
    }
}
