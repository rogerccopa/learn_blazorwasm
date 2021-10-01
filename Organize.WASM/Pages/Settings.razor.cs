using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public partial class Settings : ComponentBase
    {
        [Inject]
        private IUserItemManager userItemManager { get; set; }

        [Inject]
        private ICurrentUserService currentUserService { get; set; }

        private async void DeleteAllDone()
        {
            await userItemManager.DeleteAllDoneAsync(currentUserService.CurrentUser);
        }
    }
}
