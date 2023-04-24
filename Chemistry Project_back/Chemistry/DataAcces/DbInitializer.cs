using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
namespace DataAcces
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),

                    });
                }
            }
          var userDb=await userManager.FindByNameAsync("Faigrasul");
            if (userDb == null)
            {
                var user = new User
                {
                    Name = "Faig",
                    Surname="Rasulzadeh",
                    UserName = "Faigrasul",
                    Email = "FaigResull@gmail.com"

                };
                var result = await userManager.CreateAsync(user,"Admin1234!");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
        }
    }
}
