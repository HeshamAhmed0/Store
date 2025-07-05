using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Reposatory;
using Persistence;

namespace Store.API.Extentions.Infrustructure
{
    public static class InfrustructureServiserRegister
    {
        public static IServiceCollection InfrustructureServises(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
