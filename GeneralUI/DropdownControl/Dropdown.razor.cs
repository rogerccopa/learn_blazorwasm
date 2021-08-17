using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralUI.DropdownControl
{
    public partial class Dropdown<T> : ComponentBase
    {
        [Parameter]
        public IList<DropdownItem<T>> SelectableItems { get; set; }

        // define property for the two-way binding (value & valueChanged)
        [Parameter]
        public DropdownItem<T> SelectedItem { get; set; }
        [Parameter]
        public EventCallback<DropdownItem<T>> SelectedItemChanged { get; set; }

        public async void OnItemClicked(DropdownItem<T> item)
        {
            SelectedItem = item;

            // Tell Blazor, the control state has changed and needs to be re-rendered
            StateHasChanged();

            await SelectedItemChanged.InvokeAsync(item);
        }
    }
}
