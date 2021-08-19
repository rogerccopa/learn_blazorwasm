using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.BusinessLogic
{
    public class UserManager : IUserManager
    {
        public async Task<User> TrySignInAndGetUserAsync(User user)
        {
            await Task.Delay(3000); // simulate long task
            Console.WriteLine("Hi from UserManager");

            return await Task.FromResult(new User());
        }

        public async Task InsertUserAsync(User user)
        {
            await Task.FromResult(true);
        }
    }
}
