using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Organize.BusinessLogic;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public class SignInBase : SignBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IUserManager UserManager { get; set; }

        protected string Day { get; } = DateTime.Now.DayOfWeek.ToString();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // initialize a dummy User so for now we avoid required fields errors.
            User = new User
            {
                FirstName = "Test",
                LastName = "Last",
                PhoneNumber = "1234567"
            };

            // reInitialize the EditContext with the new User content
            MyEditContext = new EditContext(User);
        }

        protected async void MyOnSubmit()
        {
            if (!MyEditContext.Validate())
            {
                Console.WriteLine("EditContext invalid");
                foreach (var valMessage in MyEditContext.GetValidationMessages())
                {
                    Console.WriteLine(valMessage);
                }

                return;
            }

            //var userManager = new UserManager(); This is injected
            var userFound = await UserManager.TrySignInAndGetUserAsync(User);

            if (userFound != null)
            {
                NavigationManager.NavigateTo("items");
            }
        }
    }
}
