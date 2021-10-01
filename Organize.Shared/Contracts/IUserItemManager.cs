using Organize.Shared.Entities;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Shared.Contracts
{
    public interface IUserItemManager
    {
        public Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent);
        public Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(User user, ItemType typeEnum);
        Task RetrieveAllUserItemsOfUserAndSetToUserAsync(User user);
        Task UpdateAsync<T>(T item) where T : BaseItem;
    }
}
