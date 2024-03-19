using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(UserManager<IdentityUser> userManager,
               RoleManager<IdentityRole> roleManager,
               ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            // apply migrations if they are not applied
            
            try
            {
                if(_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }

            //create role if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Costomer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Costomer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

                // if users are not created, we need to create admin user

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "askaroralxan@gmail.com",
                    Email = "askaroralxan@gmail.com",
                    Name = "Askar Oralkhan",
                    PhoneNumber = "12345678900",
                    StreetAdress = "test 123 ave.",
                    State = "ALA",
                    PostalCode = "00010",
                    City = "Almaty"
                }, "Admin123!").GetAwaiter().GetResult();

                ApplicationUser user = _dbContext.applicationUsers.FirstOrDefault(u => u.Email == "askaroralxan@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
