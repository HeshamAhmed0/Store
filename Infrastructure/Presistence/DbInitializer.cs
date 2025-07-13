using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext storeDbContext;
        private readonly StoreIdentityDbContext storeIdentityDbContext;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;

        public DbInitializer(StoreDbContext storeDbContext,
                             StoreIdentityDbContext storeIdentityDbContext,
                             Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager,
                             Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager
                              )
        {
            this.storeDbContext = storeDbContext;
            this.storeIdentityDbContext = storeIdentityDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        public async Task InitializeIdentityAsync()
        {

            if (storeIdentityDbContext.Database.GetPendingMigrations().Any())
            {
                await storeIdentityDbContext.Database.MigrateAsync();
            }
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });
            }
            if (!userManager.Users.Any())
            {
                var SuperAdmin = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "superadmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01503032660"
                };
                var Admin = new AppUser()
                {
                    DisplayName = " Admin",
                    Email = "admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01500049390"
                };
                await userManager.CreateAsync(SuperAdmin, "HeshamAhmed1#");
                await userManager.CreateAsync(Admin, "HeshamAhmed1#");

                await userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
                await userManager.AddToRoleAsync(Admin, "Admin");
            }
           
        }
    }
}
