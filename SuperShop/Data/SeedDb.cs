using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supershop.Data.Entities;
using Supershop.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Supershop.Data
{
    public class SeedDb
    {

        private readonly DataContext context;
        private DataContext _context;
        private readonly IUserHelper _userHelper;
        //private readonly UserManager<User> _userManager;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        // Constructor to initialize the SeedDb class with a DataContext and UserManager
        {
            _context = context;
            _userHelper = userHelper;
            //_userManager = userManager;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            // Check if there are any users in the database
            //var user = await _userManager.FindByEmailAsync("felipe.g.sales1985@gmail.com"); // Check if a user with an empty email exists


            // Ensure admin user exists with correct email
            var user = await _userHelper.GetUserByEmailAsync("admin@supershop.com");
            if (user == null)
            {
                // Create new admin user
                user = new User
                {
                    FirstName = "Admin",
                    LastName = "Supershop",
                    Email = "admin@supershop.com",
                    UserName = "admin@supershop.com",
                    PhoneNumber = "234567891",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Could not create user in seeding database: {errors}");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
            else
            {
                // User exists, ensure they have Admin role
                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Admin");
                }
            }

            // Check if there are any products in the database
            if (!_context.Products.Any())
            {
                // When I create products now I have to say who created them
                AddProduct("IPhone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("IWatch Series 4", user);
                AddProduct("IPad Mini", user);

                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {

                Name = name,
                Price = _random.Next(100, 1000),
                IsAvailable = true,
                Stock = _random.Next(1, 100),
                // Setting the user who created the product
                User = user
            });


        }
    }
}

