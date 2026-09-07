using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            // Seed Roles
            string[] roles =
            {
                "Admin",
                "Manager",
                "Teacher",
                "Employee"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var roleResult =
                        await roleManager.CreateAsync(
                            new IdentityRole(role));

                    if (!roleResult.Succeeded)
                    {
                        throw new Exception(
                            $"Failed to create role: {role}");
                    }
                }
            }

            // Seed Admin User
            const string adminEmail = "admin@school.com";
            const string adminPassword = "Admin@123";

            var admin =
                await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Administrator"
                };

                var userResult =
                    await userManager.CreateAsync(
                        admin,
                        adminPassword);

                if (!userResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        userResult.Errors.Select(e => e.Description));

                    throw new Exception(
                        $"Failed to create admin user: {errors}");
                }
            }

            // Ensure Admin Role
            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                var roleResult =
                    await userManager.AddToRoleAsync(
                        admin,
                        "Admin");

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        "Failed to assign Admin role.");
                }
            }
        }
    }
}