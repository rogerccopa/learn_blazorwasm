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

        public static void CreateTestUser()
        {
            var user = new User();
            user.Id = 123;
            user.UserName = "rogerccopa";
            user.FirstName = "Roger";
            user.LastName = "Ccopa";
            user.Password = "test";
            user.GenderType = GenderType.Male;
            user.UserItems = new ObservableCollection<BaseItem>();

            var textItem = new TextItem();
            textItem.ParentId = user.Id;
            user.UserItems.Add(textItem);
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "From New Zealand preferred";
            textItem.ItemType = ItemType.Text;
            textItem.Position = 1;

            var urlItem = new UrlItem;
            urlItem.ParentId = user.Id;
            user.UserItems.Add(urlItem);
            urlItem.Id = 2;
            urlItem.Title = "Buy Sunflowers";
            urlItem.Url = "https://drive.google.com/file/d/1NXiNFLEUGUiNykyzdHDtf-HDocFh3OJ0/view?usp-sharing";
            urlItem.ItemType = ItemType.Url;
            urlItem.Position = 2;

            var parentItem = new ParentItem();
            parentItem.ParentId = user.Id;
            user.UserItems.Add(parentItem);
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemType = ItemType.Parent;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();

            var childItem = new ChildItem();
            childItem.ParentId = parentItem.Id;
            parentItem.ChildItems.Add(childItem);
            childItem.Id = 4;
            childItem.Position = 1;
            childItem.Title = "Cut";

            TestUser = user;
        }
    }
}
