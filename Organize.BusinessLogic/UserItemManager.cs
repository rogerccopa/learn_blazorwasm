using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.BusinessLogic
{
    public class UserItemManager : IUserItemManager
    {
        private IItemDataAccess _itemDataAccess;

        public UserItemManager(IItemDataAccess itemDataAccess)
        {
            _itemDataAccess = itemDataAccess;
        }

        public async Task RetrieveAllUserItemsOfUserAndSetToUserAsync(User user)
        {
            var allItems = new List<BaseItem>();

            var textItems = await _itemDataAccess.GetItemsOfUserAsync<TextItem>(user.Id);
            var urlItems = await _itemDataAccess.GetItemsOfUserAsync<UrlItem>(user.Id);
            var parentItems = await _itemDataAccess.GetItemsOfUserAsync<ParentItem>(user.Id);

            var parentItemsList = parentItems.ToList();
            foreach (var parentItem in parentItemsList)
            {
                var childItems = await _itemDataAccess.GetItemsOfUserAsync<ChildItem>(parentItem.Id);
                parentItem.ChildItems = new ObservableCollection<ChildItem>(childItems.OrderBy(c => c.Position));
            }

            allItems.AddRange(textItems);
            allItems.AddRange(urlItems);
            allItems.AddRange(parentItemsList);

            user.UserItems = new ObservableCollection<BaseItem>(allItems.OrderBy(i => i.Position));
        }

        public async Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent)
        {
            var childItem = new ChildItem();
            childItem.ParentId = parent.Id;
            childItem.ItemType = Shared.Enums.ItemType.Child;

            //await _itemDataAccess.InsertItemAsync<ChildItem>(childItem);
            // no need to specify InsertItemAsync<ChildItem>(...)
            // the compiler recognizes the Type automatically from the argument 'childItem'
            await _itemDataAccess.InsertItemAsync(childItem);

            parent.ChildItems.Add(childItem);
            return childItem;
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

            //await _itemDataAccess.InsertItemAsync<T>(item);
            // no need to specify InsertItemAsync<T>(...)
            // the compiler recognizes the Type automatically from the argument 'item'
            await _itemDataAccess.InsertItemAsync(item);

            return item;
        }

        public async Task UpdateAsync<T>(T item) where T : BaseItem
        {
            switch (item)
            {
                case TextItem textItem:
                    await _itemDataAccess.UpdateItemAsync(textItem);
                    break;
                case UrlItem urlItem:
                    await _itemDataAccess.UpdateItemAsync(urlItem);
                    break;
                case ParentItem parentItem:
                    await _itemDataAccess.UpdateItemAsync(parentItem);
                    break;
                case ChildItem childItem:
                    await _itemDataAccess.UpdateItemAsync(childItem);
                    break;
            }
        }

        public async Task DeleteAllDoneAsync(User user)
        {
            var doneItems = user.UserItems.Where(i => i.IsDone).ToList();
            Console.WriteLine("Done items count: " + doneItems.Count);

            var doneParentItem = doneItems.OfType<ParentItem>().ToList();
            var allChildItems = doneParentItem.SelectMany(p => p.ChildItems).ToList();

            await _itemDataAccess.DeleteItemsAsync(allChildItems);
            await _itemDataAccess.DeleteItemsAsync(doneParentItem);
            await _itemDataAccess.DeleteItemsAsync(doneItems.OfType<TextItem>());
            await _itemDataAccess.DeleteItemsAsync(doneItems.OfType<UrlItem>());

            foreach (var doneItem in doneItems)
            {
                user.UserItems.Remove(doneItem);
            }

            var sortedByPosition = user.UserItems.OrderBy(i => i.Position);
            var position = 1;
            foreach (var item in sortedByPosition)
            {
                item.Position = position;
                position++;
                await UpdateAsync(item);
            }
        }
    }
}
