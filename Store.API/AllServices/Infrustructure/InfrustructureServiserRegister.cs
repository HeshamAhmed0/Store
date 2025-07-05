using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Reposatory;
using Persistence;
using StackExchange.Redis;

namespace Store.API.Extentions.Infrustructure
{
    public static class InfrustructureServiserRegister
    {
        public static IServiceCollection InfrustructureServises(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            {
               return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            });
            return services;
        }
    }
}
