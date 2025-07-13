using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Reposatory;
using Persistence;
using StackExchange.Redis;
using Persistence.Identity;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Store.API.Extentions.Infrustructure
{
    public static class InfrustructureServiserRegister
    {
        public static IServiceCollection InfrustructureServises(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICachReposatory, CachReposatory>();
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            {
               return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            });
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }
    }
}
