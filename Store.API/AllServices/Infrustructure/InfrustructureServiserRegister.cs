using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Reposatory;
using Persistence;
using StackExchange.Redis;
using Persistence.Identity;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System.Text;

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

            #region JWT Tooken
            var JwtOptions = configuration.GetSection("JWTOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,


                    ValidIssuer = JwtOptions.Issuer,
                    ValidAudience = JwtOptions.Audiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecurityKey)),
                };
            }); 
            #endregion
            return services;
        }
    }
}
