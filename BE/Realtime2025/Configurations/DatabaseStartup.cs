
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Realtime.Domain.Entity;
using Realtime.Infrastructure;

namespace Realtime2025.Configurations
{
    public static class DatabaseStartup
    {
        public static IApplicationBuilder UseApplicationDatabase<T>(this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            bool isMig)
            where T : DbContext
        {
            if (isMig)
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<T>();
                context.Database.OpenConnection();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            return app;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app,
            IServiceProvider serviceProvider, IConfiguration configuration, bool isSeed)
        {
            if (isSeed)
            {
                using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                // Check for every context
                // Using configuration for data
                var context = scope.ServiceProvider.GetRequiredService<RealtimeDbContext>();
                context.Seed();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                AddRoles(roleManager).Wait();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                SeedAdminUser(userManager).Wait();
                // Go on with other context
            }
            return app;
        }

        private static async Task AddRoles(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = { "Admin","Member", "User"};
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }
        }   
        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}