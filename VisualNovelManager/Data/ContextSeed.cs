using Microsoft.AspNetCore.Identity;
using VisualNovelManager.Models;

namespace VisualNovelManager.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));
        }
    }
}
