using GeneralUI.DropdownControl;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Enums;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public partial class ItemsOverview : ComponentBase
    {
        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        //[Inject]
        //private ItemEditService ItemEditService { get; set; }
        [Parameter]
        public string TypeString { get; set; }
        [Parameter]
        public int? Id { get; set; }

        private DropdownItem<ItemType> SelectedDropDownType { get; set; }
        private IList<DropdownItem<ItemType>> DropDownTypes { get; set; }

        private bool ShowEdit { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //ItemEditService.EditItemChanged += HandleEditItemChanged;

            DropDownTypes = new List<DropdownItem<ItemType>>();

            var item = new DropdownItem<ItemType>();
            item.ItemObject = ItemType.Text;
            item.DisplayText = "Text";
            DropDownTypes.Add(item);

            item = new DropdownItem<ItemType>();
            item.ItemObject = ItemType.Url;
            item.DisplayText = "Url";
            DropDownTypes.Add(item);

            item = new DropdownItem<ItemType>();
            item.ItemObject = ItemType.Parent;
            item.DisplayText = "Parent";
            DropDownTypes.Add(item);
        }

        // this method is invoked every time Query Params change
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Id != null && Enum.TryParse(typeof(ItemType), TypeString, out _))
            {
                ShowEdit = true;
            }
            else
            {
                ShowEdit = false;
            }
        }

        private void HandleEditItemChanged(object sender, ItemEditEventArgs e)
        {
            ShowEdit = e.Item != null;
            StateHasChanged();
        }

        private async void AddNewAsync()
        {
            if (SelectedDropDownType ==null)
            {
                return;
            }

            await UserItemManager.CreateNewUserItemAndAddItToUserAsync(
                CurrentUserService.CurrentUser, 
                SelectedDropDownType.ItemObject);
        }
    }
}
