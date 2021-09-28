using Organize.Shared.Entities;
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
    }
}
