using KASHOP.DAL.Data;
using KASHOP.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Utilities
{
    public class SeedData : ISeedData
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(
            AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Entities.Category { Name = "clothes" },
                    new Entities.Category { Name = "Mobiles" }
                 );
            }

            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Entities.Brand { Name = "Samsung", MainImage = "samsung.jpg" },
                    new Entities.Brand { Name = "Apple" , MainImage = "apple.webp" },
                    new Entities.Brand { Name = "Huwawi" , MainImage = "huwawi.webp" }
                    );
            }

            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "alaa@gmail.com",
                    FullName = "Alaa Rabaya",
                    PhoneNumber = "1234567890",
                    UserName = "arabaya",
                    EmailConfirmed = true
                };
                var user2 = new ApplicationUser()
                {
                    Email = "ahmed@gmail.com",
                    FullName = "Ahmed Rabaya",
                    PhoneNumber = "1234567890",
                    UserName = "ahrabaya",
                    EmailConfirmed = true
                };
                var user3 = new ApplicationUser()
                {
                    Email = "islam@gmail.com",
                    FullName = "Islam Rabaya",
                    PhoneNumber = "1234567890",
                    UserName = "Irabaya",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user1, "Pass@1212");
                await _userManager.CreateAsync(user2, "Pass@1212");
                await _userManager.CreateAsync(user3, "Pass@1212");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");
            }
            await _context.SaveChangesAsync();
            
        }
    }
}
