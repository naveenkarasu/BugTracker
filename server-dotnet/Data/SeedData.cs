using BugTracker.API.Models;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.API.Data;

public static class SeedData
{
    public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create roles
        string[] roles = { "Admin", "Developer", "Tester", "Viewer" };
        
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Create admin user
        var adminUser = new ApplicationUser
        {
            UserName = "admin@bugtracker.com",
            Email = "admin@bugtracker.com",
            FirstName = "System",
            LastName = "Administrator",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0
        };

        if (await userManager.FindByEmailAsync(adminUser.Email) == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Create sample users
        var sampleUsers = new[]
        {
            new { Email = "developer@bugtracker.com", FirstName = "John", LastName = "Developer", Role = "Developer", Password = "Dev123!" },
            new { Email = "tester@bugtracker.com", FirstName = "Jane", LastName = "Tester", Role = "Tester", Password = "Test123!" },
            new { Email = "viewer@bugtracker.com", FirstName = "Bob", LastName = "Viewer", Role = "Viewer", Password = "View123!" }
        };

        foreach (var userInfo in sampleUsers)
        {
            if (await userManager.FindByEmailAsync(userInfo.Email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = userInfo.Email,
                    Email = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                var result = await userManager.CreateAsync(user, userInfo.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userInfo.Role);
                }
            }
        }
    }
} 