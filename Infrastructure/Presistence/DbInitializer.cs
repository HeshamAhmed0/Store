using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext storeDbContext;

        public DbInitializer(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
        }
        public async Task InitializeAsync()
        {
            if (storeDbContext.Database.GetPendingMigrations().Any())
            {
               await storeDbContext.Database.MigrateAsync();
            }

            if (!storeDbContext.ProductTypes.Any())
            {
                var DataOfTypes =await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");

                var PTypes = JsonSerializer.Deserialize<List<ProductType>>(DataOfTypes);

                if (PTypes is not null && PTypes.Any())
                {
                   await storeDbContext.ProductTypes.AddRangeAsync(PTypes);
                   await storeDbContext.SaveChangesAsync();
                }

            }
            if (!storeDbContext.ProductBrands.Any())
            {
                var DataOfBrands =await File.ReadAllTextAsync(@"..\\Infrastructure\\Presistence\\Data\\Seeding\\brands.json");
                var PBrand = JsonSerializer.Deserialize<List<ProductBrand>>(DataOfBrands);
                if(PBrand is not null && PBrand.Any())
                {
                    await storeDbContext.ProductBrands.AddRangeAsync(PBrand);
                    await storeDbContext.SaveChangesAsync();
                }
            }
            if (!storeDbContext.Products.Any())
            {
                var DataOfProducts = await File.ReadAllTextAsync(@"..\\Infrastructure\\Presistence\\Data\\Seeding\\products.json");
                var Jproduct=JsonSerializer.Deserialize<List<Product>>(DataOfProducts);
                if(Jproduct is not null && Jproduct.Any())
                {
                    await storeDbContext.Products.AddRangeAsync(Jproduct);
                    await storeDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
