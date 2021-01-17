namespace LipitaSystems.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LipitaSystems.Common;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var adminEmail = "lipitasystems@gmail.com";

            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = new ApplicationUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
            };

            await userManager.CreateAsync(admin, "Lipita688356!");
            await dbContext.SaveChangesAsync();
            await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
        }
    }
}
