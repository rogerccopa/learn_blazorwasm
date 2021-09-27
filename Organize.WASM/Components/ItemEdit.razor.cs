using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ItemEdit
    {
        [Inject]
        private ItemEditService ItemEditService { get; set; }

        private BaseItem Item { get; set; } = new BaseItem();
        private int TotalNumber { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ItemEditService.EditItemChanged += HandleEditItem;
            Item = ItemEditService.EditItem;
        }

        private void HandleEditItem(object sender, ItemEditEventArgs e)
        {
            Item = e.Item;
            StateHasChanged();
        }
    }
}
