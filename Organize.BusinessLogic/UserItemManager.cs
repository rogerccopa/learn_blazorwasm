using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.BusinessLogic
{
    public class UserItemManager : IUserItemManager
    {
        public async Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent)
        {
            var childItem = new ChildItem();
            childItem.ParentId = parent.Id;
            childItem.ItemType = Shared.Enums.ItemType.Child;

            parent.ChildItems.Add(childItem);
            return await Task.FromResult(childItem);
        }
    }
}
