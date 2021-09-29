using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
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

        public async Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(User user, ItemType typeEnum)
        {
            BaseItem item = null;
            switch (typeEnum)
            {
                case ItemType.Text:
                    item = await CreateAndInsertItemAsync<TextItem>(user, typeEnum);
                    break;
                case ItemType.Url:
                    item = await CreateAndInsertItemAsync<UrlItem>(user, typeEnum);
                    break;
                case ItemType.Parent:
                    var parent = await CreateAndInsertItemAsync<ParentItem>(user, typeEnum);
                    parent.ChildItems = new System.Collections.ObjectModel.ObservableCollection<ChildItem>();
                    item = parent;
                    break;
            }

            user.UserItems.Add(item);
            return item;
        }

        private async Task<T> CreateAndInsertItemAsync<T>(User user, ItemType typeEnum) where T : BaseItem, new()
        {
            var item = new T();
            item.ItemType = typeEnum;
            item.Position = user.UserItems.Count + 1;
            item.ParentId = user.Id;
            // TODO add to Storage provider

            return await Task.FromResult(item);
        }
    }
}
