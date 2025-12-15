
namespace Hotel_Room_Booking_system.Seeding
{
    public class Seeding
    {
        public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            // Roles
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Default Admin User
            var adminEmail = "admin@gmail.com";
            var AdminPassword = "Admin@123";
            var Address = "Alex";
            var PhoneNumber = "0123456789";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    NormalizedUserName=
                    Address = Address,
                    PhoneNumber = PhoneNumber,
                    Roles = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, AdminPassword);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}