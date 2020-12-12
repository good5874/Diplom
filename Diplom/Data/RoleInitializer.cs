using Microsoft.AspNetCore.Identity;
using Diplom.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin1@gmail.com";
            string password = "admin1@gmail.comQWE";

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Driver") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Driver"));
            }
            if (await roleManager.FindByNameAsync("Manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }
            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                Users admin = new Users { Email = adminEmail, UserName = adminEmail };
                admin.EmailConfirmed = true;
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
