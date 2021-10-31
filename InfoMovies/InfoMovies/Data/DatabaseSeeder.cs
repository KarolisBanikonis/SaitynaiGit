using InfoMovies.Auth.Model;
using InfoMovies.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data
{
    public class DatabaseSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<InfoMoviesUser> _userManager;

        public DatabaseSeeder(UserManager<InfoMoviesUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            foreach(var role in InfoMoviesUserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var newAdminUser = new InfoMoviesUser
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if(existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, InfoMoviesUserRoles.All);
                }
            }
        }
    }
}
