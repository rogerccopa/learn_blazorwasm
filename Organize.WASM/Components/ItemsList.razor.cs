﻿using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ItemsList : ComponentBase
    {
        [Inject]
        public ICurrentUserService CurrentUserService { get; set; }

        protected ObservableCollection<BaseItem> UserItems { get; set; } = new ObservableCollection<BaseItem>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UserItems = CurrentUserService.CurrentUser.UserItems;
            Console.WriteLine(UserItems.Count);
            Console.WriteLine(UserItems);
            Console.WriteLine(JsonSerializer.Serialize(UserItems));
        }
    }
}
