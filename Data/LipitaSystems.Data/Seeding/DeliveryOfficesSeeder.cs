namespace LipitaSystems.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Models;
    using Newtonsoft.Json;

    public class DeliveryOfficesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DeliveryOffices.Any())
            {
                return;
            }

            var officesJson = File.ReadAllText(Path.Combine("..", "..", "Data", "LipitaSystems.Data", "Seeding", "Data", "Offices.json"));
            var deserializedOffices = JsonConvert.DeserializeObject<List<DeliveryOffice>>(officesJson);

            await dbContext.AddRangeAsync(deserializedOffices);
            await dbContext.SaveChangesAsync();
        }
    }
}
