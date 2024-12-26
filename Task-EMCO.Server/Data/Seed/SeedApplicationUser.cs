namespace Task_EMCO.Server.Data.Seed;

public class SeedApplicationUser
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
    {
        var user = await userManager.FindByEmailAsync("admin@emco.com");

        if (user == null)
        {
            user = new ApplicationUser
            {
                Email = "admin@emco.com",
                UserName = "admin",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "P@ssw0rd");
        }
    }
}
