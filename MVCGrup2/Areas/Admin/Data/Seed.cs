using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Data;
using System.Net;

namespace MVCGrup2.Areas.Admin.Data
{
    public class Seed
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<MVCGrup2User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public Seed(IServiceProvider serviceProvider, UserManager<MVCGrup2User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateAdminIfNotExist()
        {
            try
            {

                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var adminUser = await _userManager.FindByNameAsync("admin@example.com");
                if (adminUser == null)
                {
                    adminUser = new MVCGrup2User
                    {
                        Name = "Admin",
                        Surname = "Admin",
                        UserName = "admin@burgeristan.com",
                        Email = "admin@burgeristan.com",
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(adminUser, "Admin1$");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("An error ocurred: " + ex);
            }

        }
    }
}
