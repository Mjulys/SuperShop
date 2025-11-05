using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;



namespace SuperShop.Helpers
{
    public interface IUserHelper
    {
        Task<User>  GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
