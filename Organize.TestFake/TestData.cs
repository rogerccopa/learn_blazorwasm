using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.TestFake
{
    public class TestData
    {
        public static User TestUser { get; private set; }

        public static void CreateTestUser(IUserItemManager userItemManager = null)
        {
            var user = new User();
            user.Id = 123;
            user.UserName = "rogerccopa";
            user.FirstName = "Roger";
            user.LastName = "Ccopa";
            user.Password = "test";
            user.GenderType = GenderType.Male;
            user.UserItems = new ObservableCollection<BaseItem>();

            TextItem textItem;

            if (userItemManager != null)
            {
                textItem = (TextItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemType.Text).Result;
            }
            else
            {
                textItem = new TextItem();
                user.UserItems.Add(textItem);
            }

            textItem.ParentId = user.Id;
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "Red | 5";
            textItem.Detail = "From New Zealand preferred";
            textItem.ItemType = ItemType.Text;
            textItem.Position = 1;

            UrlItem urlItem;

            if (userItemManager != null)
            {
                urlItem = (UrlItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemType.Url).Result;
            }
            else
            {
                urlItem = new UrlItem();
                user.UserItems.Add(urlItem);
            }

            urlItem.ParentId = user.Id;
            urlItem.Id = 2;
            urlItem.Title = "Buy Sunflowers";
            urlItem.Url = "https://drive.google.com/file/d/1NXiNFLEUGUiNykyzdHDtf-HDocFh3OJ0/view?usp-sharing";
            urlItem.ItemType = ItemType.Url;
            urlItem.Position = 2;

            ParentItem parentItem;

            if (userItemManager != null)
            {
                parentItem = (ParentItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemType.Parent).Result;
            }
            else
            {
                parentItem = new ParentItem();
                user.UserItems.Add(parentItem);
            }

            parentItem.ParentId = user.Id;
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemType = ItemType.Parent;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();

            ChildItem childItem;

            if (userItemManager != null)
            {
                childItem = (ChildItem)userItemManager.CreateNewChildItemAndAddItToParentItemAsync(parentItem).Result;

                user.UserItems.Clear();
            }
            else
            {
                childItem = new ChildItem();
                parentItem.ChildItems.Add(childItem);
            }

            childItem.ParentId = parentItem.Id;
            childItem.Id = 4;
            childItem.Position = 1;
            childItem.Title = "Cut";

            TestUser = user;
        }
    }
}
