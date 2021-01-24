namespace LipitaSystems.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Models;
    using Newtonsoft.Json;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MainCategories.Any())
            {
                return;
            }

            var categoriesJson = File.ReadAllText(Path.Combine("..", "..", "site", "wwwroot", "Seeding", "Data", "Categories.json"));
            var deserializedCategories = JsonConvert.DeserializeObject<List<MainCategory>>(categoriesJson);

            await dbContext.MainCategories.AddRangeAsync(deserializedCategories);
            await dbContext.SaveChangesAsync();
        }
    }
}
