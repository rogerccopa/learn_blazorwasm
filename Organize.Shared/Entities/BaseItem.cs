using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Shared.Entities
{
    public class BaseItem : BaseEntity
    {
        public int ParentId { 
            get => _parentId; 
            set => SetProperty<int>(ref _parentId, value); 
        }
        private int _parentId;

        public ItemType ItemType { 
            get => _itemType; 
            set => SetProperty<ItemType>(ref _itemType, value); 
        }
        private ItemType _itemType;

        public int Position { 
            get => _position;
            set => SetProperty<int>(ref _position, value); 
        }
        private int _position;

        public bool IsDone { 
            get => _isDone; 
            set => SetProperty(ref _isDone, value); 
        }
        private bool _isDone;

        public string Title { 
            get => _title; 
            set => SetProperty(ref _title, value); 
        }
        private string _title;
    }
}
