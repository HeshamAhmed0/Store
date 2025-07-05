using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Store.API.Extentions.Infrustructure;
using Store.API.Extentions.MidelwareServices;
using Store.API.Extentions.Services;

namespace Store.API.Extentions.ApplyForAllServices
{
    public static  class ApplyForAllServices
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.InfrustructureServises(configuration);
            services.ApplyAllicationServices();
            ValidationConfigrationServices(services);

            return services;
        }

        private static void ValidationConfigrationServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var ErrorResponse = actionContext.ModelState.Where(m => m.Value.Errors.Any()).
                                              Select(m => new ValidationError()
                                              {
                                                  Field = m.Key,
                                                  Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToList(),

                                              }).ToList();

                    var response = new ValidationErrorResponse()
                    {
                        errors = ErrorResponse,
                    };
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
