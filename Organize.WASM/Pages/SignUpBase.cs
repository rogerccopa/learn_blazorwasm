using GeneralUI.DropdownControl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Organize.Shared.Enums;
using System.Collections.Generic;

namespace Organize.WASM.Pages
{
    public class SignUpBase : SignBase
    {
        [Parameter]
        public string Username { get; set; }

        protected IList<DropdownItem<GenderType>> GenderTypeItems { get; } = new List<DropdownItem<GenderType>>();

        protected DropdownItem<GenderType> SelectedGenderTypeItem { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var male = new DropdownItem<GenderType>
            {
                ItemObject = GenderType.Male,
                DisplayText = "Male"
            };
            var female = new DropdownItem<GenderType>
            {
                ItemObject = GenderType.Female,
                DisplayText = "Female"
            };
            var neutral = new DropdownItem<GenderType>
            {
                ItemObject = GenderType.Neutral,
                DisplayText = "Others"
            };

            GenderTypeItems.Add(male);
            GenderTypeItems.Add(female);
            GenderTypeItems.Add(neutral);

            SelectedGenderTypeItem = female;

            //TryGetUsernameFromUri();
            User.UserName = Username;
        }

        private void TryGetUsernameFromUri()
        {
            System.Uri uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            StringValues sv;
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("username", out sv))
            {
                User.UserName = sv;
            }
        }
    }
}
