using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralUI.DropdownControl;
using Organize.Shared.Enums;

namespace Organize.WASM.Pages
{
    public class SignUpBase : SignBase
    {
        protected IList<DropdownItem<GenderType>> GenderTypeItems { get; } = new List<DropdownItem<GenderType>>();

        protected DropdownItem<GenderType> SelectedGenderTypeItem { get; set; }

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
        }
    }
}
