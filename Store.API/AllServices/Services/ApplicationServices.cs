using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Reposatory;
using Services;
using Services_Absractions;

namespace Store.API.Extentions.Services
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplyAllicationServices(this IServiceCollection services)
        {
           services.AddScoped<IServiceManager, ServicesManager>();

           services.AddAutoMapper(typeof(AssemplyReference).Assembly);
            return services;
        }
    }
}
