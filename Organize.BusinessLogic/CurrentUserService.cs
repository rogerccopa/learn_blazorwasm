using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.BusinessLogic
{
    public class CurrentUserService : ICurrentUserService
    {
        public User CurrentUser { get; set; }
    }
}
