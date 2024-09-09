

using Microsoft.Extensions.DependencyInjection;

namespace FinalBoatSystemRental.Infrastructure.Seeding;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure database is created
            context.Database.EnsureCreated();


            // Seed Users
            await SeedDefaultUserAsync(userManager);
        }
    }



    private static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
    {

        var user = new ApplicationUser
        {
            FirstName = "Admin",
            LastName = "Admin",
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, GlobalVariables.AdminPassword);


        await userManager.AddToRoleAsync(user, GlobalVariables.Admin);
    }
}
