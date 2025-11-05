using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


namespace SuperShop.Helpers
{
    public class UserHelper : IUserHelper
    {

        private readonly UserManager<User> _userManager;

        public UserHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        UserManager<User> UserManager { get; }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user,password);

        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
