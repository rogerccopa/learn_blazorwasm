using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Organize.BusinessLogic;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

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

        [Inject]
        private IUserItemManager userItemManager { get; set; }

        private BaseItem Item { get; set; } = new BaseItem();
        private int TotalNumber { get; set; }

        private Timer _debounceTimer;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _debounceTimer = new Timer();
            _debounceTimer.Interval = 500;
            _debounceTimer.AutoReset = false;
            _debounceTimer.Elapsed += HandleDebounceTimerElapsed;

            //ItemEditService.EditItemChanged += HandleEditItem;
            //Item = ItemEditService.EditItem;
            SetDataFromUri();
        }

        private void HandleDebounceTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer elapsed");
            userItemManager.UpdateAsync(Item);
        }

        //private void HandleEditItem(object sender, ItemEditEventArgs e)
        //{
        //    Item = e.Item;
        //    StateHasChanged();
        //}

        private void SetDataFromUri()
        {
            if (Item != null)
            {
                Item.PropertyChanged -= HandleItemPropertyChanged;
            }

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
                    Item.PropertyChanged += HandleItemPropertyChanged;
                    MyNavigationManager.LocationChanged += HandleLocationChanged;
                    StateHasChanged();
                }
            }
        }

        private void HandleItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //await userItemManager.UpdateAsync(Item);

            if (_debounceTimer != null)
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
        }

        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            SetDataFromUri();
        }

        public void Dispose()
        {
            _debounceTimer?.Dispose();
            MyNavigationManager.LocationChanged -= HandleLocationChanged;
            Item.PropertyChanged -= HandleItemPropertyChanged;
        }

        //private void HandleEditItemChanged(object sender, ItemEditEventArgs e)
        //{
        //    if (Item != null)
        //    {
        //        Item.PropertyChanged -= HandleItemPropertyChanged;
        //    }

        //    Item = e.Item;
        //    Item.PropertyChanged += HandleItemPropertyChanged;
        //    StateHasChanged();
        //}
    }
}
