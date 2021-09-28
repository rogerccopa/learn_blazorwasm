using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ChildItemEdit : ComponentBase
    {
        [Parameter]
        public ParentItem ParentItem { get; set; }

        [Inject]
        private IUserItemManager UserItemMananager { get; set; }

        private async void AddNewChildToParentAsync()
        {
            await UserItemMananager.CreateNewChildItemAndAddItToParentItemAsync(ParentItem);
        }
    }
}
