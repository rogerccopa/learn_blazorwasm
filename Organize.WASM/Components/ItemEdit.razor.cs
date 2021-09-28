using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Organize.BusinessLogic;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ItemEdit : ComponentBase, IDisposable
    {
        //[Inject]
        //private ItemEditService ItemEditService { get; set; }
        [Inject]
        NavigationManager MyNavigationManager { get; set; }
        [Inject]
        ICurrentUserService CurrentUserService { get; set; }

        private BaseItem Item { get; set; } = new BaseItem();
        private int TotalNumber { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //ItemEditService.EditItemChanged += HandleEditItem;
            //Item = ItemEditService.EditItem;
            SetDataFromUri();
        }

        //private void HandleEditItem(object sender, ItemEditEventArgs e)
        //{
        //    Item = e.Item;
        //    StateHasChanged();
        //}

        private void SetDataFromUri()
        {
            var uri = MyNavigationManager.ToAbsoluteUri(MyNavigationManager.Uri);
            var segmentCount = uri.Segments.Length;

            if (segmentCount > 2 && 
                Enum.TryParse(typeof(ItemType), uri.Segments[segmentCount-2].Trim('/'), out var typeEnum) &&
                int.TryParse(uri.Segments[segmentCount-1], out var id))
            {
                var userItem = CurrentUserService.CurrentUser
                    .UserItems
                    .SingleOrDefault(Item => Item.ItemType == (ItemType)typeEnum && Item.Id == id);

                // Not found? redirect to items
                if (userItem == null)
                {
                    MyNavigationManager.LocationChanged -= HandleLocationChanged;
                    MyNavigationManager.NavigateTo("/items");
                }
                else
                {
                    Item = userItem;
                    MyNavigationManager.LocationChanged += HandleLocationChanged;
                    StateHasChanged();
                }
            }
        }

        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            SetDataFromUri();
        }

        public void Dispose()
        {
            MyNavigationManager.LocationChanged -= HandleLocationChanged;
        }
    }
}
